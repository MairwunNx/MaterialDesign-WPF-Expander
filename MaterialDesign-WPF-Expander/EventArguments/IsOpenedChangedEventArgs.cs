namespace MaterialDesign_WPF_Expander.EventArguments
{
    /// <summary>
    /// Event arguments for Expander ui-element,
    /// for IsOpenedChanged event.
    /// </summary>
    public class IsOpenedChangedEventArgs
    {
        /// <summary>
        /// Set or get the expander is opened state
        /// from event args.
        /// </summary>
        public bool IsOpened { get; set; }
    }
}