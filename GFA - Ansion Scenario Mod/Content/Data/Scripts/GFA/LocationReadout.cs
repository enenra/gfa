using System;
using System.Collections.Generic;
using System.Linq;
using Sandbox.Definitions;
using Sandbox.Game;
using Sandbox.Game.Entities;
using Sandbox.ModAPI;
using VRage.Game;
using VRage.Game.Components;
using VRage.Utils;
using VRageMath;
using BlendTypeEnum = VRageRender.MyBillboard.BlendTypeEnum; // required for MyTransparentGeometry/MySimpleObjectDraw to be able to set blend type.
using RichHudFramework.UI.Rendering.Client;
using RichHudFramework.UI.FontData;
using RichHudFramework.Client;
using RichHudFramework.UI;
using RichHudFramework.UI.Client;
using RichHudFramework.UI.Rendering;

namespace LocationReadout
{

    [MySessionComponentDescriptor(MyUpdateOrder.AfterSimulation)]
    public class LocationReadout : MySessionComponentBase
    {
        public static LocationReadout Instance; // the only way to access session comp from other classes and the only accepted static field.

        private List<Region> Regions;
        private Region? currentRegion;
        IFontMin gfaAurabeshFont;
        GlyphFormat gfaAurabeshGlyph;
        IFontMin gfaStandardFont;
        GlyphFormat gfaStandardGlyph;
        List<Label> labelList = new List<Label>();
        bool isDisplaying = false;
        int ticks = 0;
        int displayTime = 600;
        Dictionary<string, int> gfaStandardOffsets = new Dictionary<string, int>(){
            {"A", 10}
            };

        public override void Init(MyObjectBuilder_SessionComponent sessionComponent)
        {
            if (!MyAPIGateway.Utilities.IsDedicated)
            {
                RichHudClient.Init("GFAFont", InitComplete, ClientReset);
            }
        }
        private void ClientReset()
        {

        }
        private void InitComplete()
        {
            FontManager.TryAddFont(GFA_Aurabesh.GetFontData(), out gfaAurabeshFont);
            gfaAurabeshGlyph = new GlyphFormat(Color.White, TextAlignment.Left, 1f, FontStyles.Regular, gfaAurabeshFont);

            FontManager.TryAddFont(GFA_Standard.GetFontData(), out gfaStandardFont);
            gfaStandardGlyph = new GlyphFormat(Color.White, TextAlignment.Left, 1f, FontStyles.Regular, gfaStandardFont);
        }

        public override void LoadData()
        {
            Instance = this;
            Regions = new List<Region>();
            currentRegion = null;
            // here we want to get data from outside the game
            /*using (var reader = MyAPIGateway.Utilities.ReadFileInModLocation("some_file_in_your_mod.txt", ModContext.ModItem))
            {
                // populate list somehow
            }*/
            // dummy data for testing, could follow this pattern and hard-code inside the .cs file
            Regions.Add(
                new Region { 
                        position = new Vector3D(0, 0, 0), 
                        radius = 5000, 
                        radiusSquared = 5000 * 5000, 
                        entrySystem = "Unknown", 
                        entryLocation = "Center", 
                        exitSystem = "Unknown", 
                        exitLocation = "Outside Center"
                    }
                );
        }


        protected override void UnloadData()
        {
            // executed when world is exited to unregister events and stuff
            Regions.Clear(); // probably not needed, but eh
            Instance = null; // important for avoiding this object to remain allocated in memory
        }


        public override void UpdateAfterSimulation()
        {
            // executed every tick, 60 times a second, after physics simulation and only if game is not paused.

            // check if its a local session, and the session has a character
            if (MyAPIGateway.Session?.Player?.Character != null)
            {
                if (!isDisplaying) {
                    ticks = 0;
                }
                else {
                    ticks++;
                }

                Vector3D currentPlayerPosition = MyAPIGateway.Session.Player.Character.GetPosition();
                // order by distance to find the points closest to the player
                // should probably use List.Find() instead or FindAll() instead.
                Regions.Sort((r, o) => (r.position - currentPlayerPosition).LengthSquared().CompareTo((o.position - currentPlayerPosition).LengthSquared()));
                var region = Regions[0];

                if (isDisplaying && ticks >= displayTime) {
                    RemoveLocationInfo();
                }

                // check if we are currently in a valid position
                if ((region.position - currentPlayerPosition).LengthSquared() < region.radiusSquared)
                {
                    // we must be in a region, since the numbers say so. and the closest one too
                    // so check if its the one we were in before
                    if (currentRegion.HasValue && region.position == currentRegion.Value.position)
                    {
                        // and then do nothing
                    }
                    else { 
                        // its if the region we are in isnt that one that we care
                        // so send the message for entering it
                        SendEntryMessage(region);
                        currentRegion = region;
                    }
                } 
                else if (currentRegion.HasValue) {
                    // since they are sorted, the first position must be one of: a region we are in, the region we were in before, or one too far away from us. the first two conditionals get the first two cases, leaving the third.
                    // send the message for exiting the region.
                    SendExitMessage(currentRegion.Value);
                    // we also need to set ourselves as *not* in a position
                    currentRegion = null;
                }
            }
        }


        // method left as an exercise for the reader
        public void SendEntryMessage(Region region)
        {
            ShowLocationInfo(region.entrySystem, region.entryLocation);
        }

        // Method left as an exercise for the reader. 
        public void SendExitMessage(Region region)
        {            
            ShowLocationInfo(region.exitSystem, region.exitLocation);
        }

        public void RemoveLocationInfo()
        {
            foreach (var label in labelList) {
                if (label != null) {
                    label.Visible = false;
                    label.Unregister();
                }
            }
            
            isDisplaying = false;
        }

        public void ShowLocationInfo(String system, String location)
        {            
            //MyAPIGateway.Utilities.ShowNotification(message);

            if (isDisplaying) {
                RemoveLocationInfo();
            }

            string systemInfo = "System: " + system;
            Vector2 startPosSystem = new Vector2(-1920/2+25, 0);
            Vector2 currentPosSystem = startPosSystem;

            string locationInfo = "Location: " + location;
            Vector2 startPosLocation = new Vector2(-1920/2+25, -40);
            Vector2 currentPosLocation = startPosLocation;

            float spacing = 20f;
            
            Label label = new Label();
            label.Register(HudMain.HighDpiRoot, true);
            label.Format = gfaStandardGlyph;
            label.TextBoard.Scale = 2f;
            label.Offset = new Vector2(-1920/2+200, 0);
            label.Text = systemInfo;
            labelList.Add(label);
            
/*             foreach (var letter in systemInfo)
            {
                Label label = new Label();
                label.Register(HudMain.HighDpiRoot, true);
                label.Format = gfaStandardGlyph;
                label.TextBoard.Scale = 2f;
                label.Offset = currentPosSystem;
                label.Text = letter.ToString();

                labelList.Add(label);
                currentPosSystem.X += spacing;
            } */

            Label label2 = new Label();
            label2.Register(HudMain.HighDpiRoot, true);
            label2.Format = gfaStandardGlyph;
            label2.TextBoard.Scale = 2f;
            label2.Offset = new Vector2(-1920/2+200, -40);
            label2.Text = locationInfo;
            labelList.Add(label2);
            
/*             foreach (var letter in locationInfo)
            {
                Label label = new Label();
                label.Register(HudMain.HighDpiRoot, true);
                label.Format = gfaStandardGlyph;
                label.TextBoard.Scale = 2f;
                label.Offset = currentPosLocation;
                label.Text = letter.ToString();

                labelList.Add(label);
                currentPosLocation.X += spacing;
            } */

            isDisplaying = true;
        }

        public override MyObjectBuilder_SessionComponent GetObjectBuilder()
        {
            // executed during world save, most likely before entities.

            return base.GetObjectBuilder(); // leave as-is.
        }

        public struct Region { 
            /// <summary>
            /// Location of the region
            /// </summary>
            public Vector3D position;
            /// <summary>
            /// how far from the center the region occupies
            /// </summary>
            public double radius;
            /// <summary>
            /// how far from the center the region occupies.. squared, for faster maths
            /// </summary>
            public double radiusSquared;

            // replace these with.. whatever you need
            public string entrySystem;
            public string entryLocation;

            public string exitSystem;
            public string exitLocation;
        }
    }


}