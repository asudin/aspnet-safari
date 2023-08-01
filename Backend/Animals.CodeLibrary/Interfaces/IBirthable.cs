namespace Animals.CodeLibrary.Interfaces
{
    public interface IBirthable
    {
        public bool IsMating { get; set; }
        public bool IsRecentlyMated { get; set; }
        public int MatingTimer { get; set; }
        public int MatingCooldown { get; set; }
        public (int Row, int Column) BirthPosition { get; set; }

        public void SetMatingProperties(bool isMating, int matingTimer, int matingCooldown)
        {
            IsMating = isMating;
            MatingTimer = matingTimer;
            MatingCooldown = matingCooldown;
        }
    }
}
