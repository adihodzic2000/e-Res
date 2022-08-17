using Common.Dto;
using Common.Dto.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IFileService
    {
        Task<Message> UploadImageAsMessageAsync(FileUploadDto fileUploadDto, CancellationToken cancellationToken);
        Task<Message> GetImagesByCompanyAsMessageAsync(CancellationToken cancellationToken);
        Task<Message> GetImagesByCompanyIdAsMessageAsync(Guid Id, CancellationToken cancellationToken);//difference between desktop and mobile user
        Task<Message> DeleteImageByCompanyAsMessageAsync(Guid Id, CancellationToken cancellationToken);
        Task<Message> ChangeMyProfilePictureAsMessageAsync(FileUploadDto fileUploadDto, CancellationToken cancellationToken);
        Task<Message> GetProfilePictureAsMessageAsync(CancellationToken cancellationToken);

    }
}
