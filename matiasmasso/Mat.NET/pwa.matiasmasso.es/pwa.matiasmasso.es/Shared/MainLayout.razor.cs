namespace pwa.matiasmasso.es.Shared
{
    public partial class MainLayout
    {
        private bool hideNavMenu = true;
        private bool hideLeftAside = false;

        private void ToggleMenu()
        {
            hideLeftAside = !hideLeftAside;
            hideNavMenu = !hideNavMenu;
        }

    }
}
