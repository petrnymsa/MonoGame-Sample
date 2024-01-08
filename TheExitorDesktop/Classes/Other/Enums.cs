namespace TheExitorDesktop
{

    /// <summary>
    /// Návratový status Messages
    /// </summary>
    public enum PopUpStatus
    {
        Yes,
        No
    };


    /// <summary>
    /// Typ tlačítek
    /// </summary>
    public enum PopUpButtons
    {
        Ok,
        YesAndNo
    };

    /// <summary>
    /// Viditelnost Tiles, Default = NoVisible
    /// </summary>
    public enum TileVisibility
    {
        NoVisible,
        Visible,        
        Explored
    };

    /// <summary>
    /// Třízení vykreslování
    /// </summary>
    public enum LayerSorting
    {
        BackToFront,
        FrontToBack,
    }
}