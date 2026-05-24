using Sandbox.Definitions;
using VRage.Game.Components;

namespace Digi.Experiments
{
    [MySessionComponentDescriptor(MyUpdateOrder.NoUpdate)]
    public class Example_RemoveFromCategory : MySessionComponentBase
    {
        public override void LoadData()
        {
            RemoveFromCategory("Thrust", "SmallAtmosphericHoverEngine_LargeBlock", "LargeBlocks");
            RemoveFromCategory("Thrust", "LargeAtmosphericHoverEngine_LargeBlock", "LargeBlocks");
            RemoveFromCategory("Thrust", "SmallAtmosphericHoverEngine_SmallBlock", "SmallBlocks");
            RemoveFromCategory("Thrust", "LargeAtmosphericHoverEngine_SmallBlock", "SmallBlocks");
        }

        void RemoveFromCategory(string typeName, string subtypeName, string categoryName)
        {
            MyGuiBlockCategoryDefinition categoryDef;
            if (MyDefinitionManager.Static.GetCategories().TryGetValue(categoryName, out categoryDef))
            {
                categoryDef.ItemIds.Remove(typeName + "/" + subtypeName);
            }
        }

        protected override void UnloadData()
        {
        }
    }
}
