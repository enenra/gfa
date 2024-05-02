using System;
using Sandbox.ModAPI;
using VRageMath;
using VRage.Game.ModAPI;
using VRage.ModAPI;
using VRage.Game;
using Sandbox.ModAPI.Ingame;
using VRage.Game.Components;
using VRage.Utils;
using IMyCockpit = Sandbox.ModAPI.Ingame.IMyCockpit;

namespace YourModNamespace
{
    [MySessionComponentDescriptor(MyUpdateOrder.BeforeSimulation | MyUpdateOrder.AfterSimulation)]
    public class BoxDrawing : MySessionComponentBase
    {
        private const int NumberOfBoxes = 10;
        private static readonly Color BoxColor = new Color(255, 0, 0, 128);
        private static readonly Color BlueBoxColor = new Color(0, 0, 255, 128);
        private static readonly Color LineColor = Color.Green;
        private static readonly int[] DistanceMultipliers = { 500, 1000, 1500, 2000, 2500, 3000, 3500, 4000, 4500, 5000 };
        private const int BoundaryRadius = 2500; // Define the boundary radius

        public override void UpdateAfterSimulation()
        {

            Vector3D? playerPosition = PlayerPosition;
            if (!playerPosition.HasValue)
            {
                return;  // If playerPosition is null, exit the method
            }

            Vector3D directionToOrigin = CalculateDirectionToOrigin(playerPosition.Value);
            double distanceToOrigin = playerPosition.Value.Length();
            double remainingDistance = BoundaryRadius - distanceToOrigin; // Calculate remaining distance
            MatrixD rotationMatrix = CalculateRotationMatrix(directionToOrigin);

            if (remainingDistance <= 150) // Adjust this value as needed
            {
                ShowDistanceNotification(remainingDistance);
            }

            if (rotationMatrix == null)
            {
                return; // If rotationMatrix is null, exit the method
            }

        }

        private void ShowDistanceNotification(double remainingDistance)
        {
            string notificationMessage = $"Out of Bounds in: {remainingDistance.ToString("F2")} meters";
            MyAPIGateway.Utilities.ShowNotification(notificationMessage, 10, MyFontEnum.Red);
        }

        private static Vector3D? PlayerPosition
        {
            get
            {
                var spectator = MyAPIGateway.Session.IsCameraUserControlledSpectator;
                var session = MyAPIGateway.Session?.Player?.Controller?.ControlledEntity?.Entity;
                var cockpit = MyAPIGateway.Session.ControlledObject?.Entity as IMyCockpit;
                if (session == null || cockpit == null && !spectator)
                {
                    // MyLog.Default.WriteLine("Null object encountered in GetPlayerPosition, or not in cockpit");
                    return null;
                }
                return session.GetPosition();
            }
        }

        private static Vector3D CalculateDirectionToOrigin(Vector3D playerPosition) => Vector3D.Normalize(Vector3D.Zero - playerPosition);

        private static MatrixD CalculateRotationMatrix(Vector3D directionToOrigin) => MatrixD.CreateWorld(Vector3D.Zero, directionToOrigin, Vector3D.Up);

        protected override void UnloadData() { }
    }
}
