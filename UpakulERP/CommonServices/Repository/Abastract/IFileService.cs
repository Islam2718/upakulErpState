using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonServices.RequestModel;
using CommonServices.ResponseModel;

namespace CommonServices.Repository.Abastract
{
    public interface IFileService
    {
        Task<FileStorageResponse> SingleFileStorage(FileStorageRequest request);
        Task<FileStorageResponse> MultipleFileStorage(FileStorageRequest request);
        bool DeleteImage(string fileNameWithLocation);
    }
}
