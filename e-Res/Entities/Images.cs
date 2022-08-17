

using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class Images : BaseEntity
    {
        public string Path { get; set; }

        [ForeignKey(nameof(UserProfilePictureId))]
        public User? UserProfilePicture { get; set; }
        public Guid? UserProfilePictureId { get; set; }
    }
}
