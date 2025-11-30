namespace DuckovController.SceneEdit.GamePlayUI
{
    public class LootViewSubPatchTarget : LootViewSubSelection
    {
        protected override string SelectorPatchSubPath => "Content";
        
        protected override void Patch()
        {
            base.Patch();
            
        }
    }
}
