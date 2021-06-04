namespace Munters.Assignment.Entities
{
    public class Images
    {
        public Original original { get; set; }
        public Downsized downsized { get; set; }
        public DownsizedLarge downsized_large { get; set; }
        public DownsizedMedium downsized_medium { get; set; }
        public DownsizedSmall downsized_small { get; set; }
        public DownsizedStill downsized_still { get; set; }
        public FixedHeight fixed_height { get; set; }
        public FixedHeightDownsampled fixed_height_downsampled { get; set; }
        public FixedHeightSmall fixed_height_small { get; set; }
        public FixedHeightSmallStill fixed_height_small_still { get; set; }
        public FixedHeightStill fixed_height_still { get; set; }
        public FixedWidth fixed_width { get; set; }
        public FixedWidthDownsampled fixed_width_downsampled { get; set; }
        public FixedWidthSmall fixed_width_small { get; set; }
        public FixedWidthSmallStill fixed_width_small_still { get; set; }
        public FixedWidthStill fixed_width_still { get; set; }
        public Looping looping { get; set; }
        public OriginalStill original_still { get; set; }
        public OriginalMp4 original_mp4 { get; set; }
        public Preview preview { get; set; }
        public PreviewGif preview_gif { get; set; }
        public PreviewWebp preview_webp { get; set; }
        public _480wStill _480w_still { get; set; }
        public Hd hd { get; set; }
    }
}