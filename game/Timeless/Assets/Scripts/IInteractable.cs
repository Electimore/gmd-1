public interface IInteractable
{
    public bool Interact(); // returns true if should be dismissed, false if not
    public void Dismiss();
}