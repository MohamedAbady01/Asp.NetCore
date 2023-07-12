using Microsoft.EntityFrameworkCore;
using System;
using Task_2Api.Data;
using Task_2Api.Models;
using static System.Net.Mime.MediaTypeNames;

namespace Task_2Api.Services.ApprovalServices
{
    public class ApprovalServices : IApproval
    {
        private readonly ApplicationDBContext _context;
        public ApprovalServices(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task CreateApprovalModel(Approvaldto model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model), "Model Not Found");
            }

            DateTime dateTime = DateTime.Now;

            string dateOnly = dateTime.ToString("yyyy-MM-dd");  // Format the datetime as "YYYY-MM-DD"

            TimeSpan timeOnly = dateTime.TimeOfDay;

            if (model.Attachments != null)
            {
                var newImages = new List<AttachmentModel>();
                var Service = new ServiceModel()
                {
                   Name = model.ServicesName,
                   Quantiity = model.ServicesQuantitty,
                };

                var Diagnosis = new DiagonsisModel()
                {

                    Name  =  model.DiagnoseName
                };
                foreach (var newImage in model.Attachments)
                {
                    using var stream = new MemoryStream();
                    await newImage.CopyToAsync(stream);
                    var imageBytes = stream.ToArray();
                    var image = new AttachmentModel
                    {
                        Images = imageBytes
                    };

                    newImages.Add(image);
                }
                var ServiceList = new List<ServiceModel>()
                {
                    Service
                };

                var DiagnosesList = new List<DiagonsisModel>() { Diagnosis };

                var approval = new ApprovalModel()
                {
                    Attachments = newImages,
                    ApprovalDate = dateOnly,
                    ApprovalTime = timeOnly,
                    Services = ServiceList,
                    Diagnoses = DiagnosesList,
                    Status = model.Status
                };

                await _context.Approvals.AddAsync(approval);
                await _context.SaveChangesAsync();


            }
            else
            {
                throw new ArgumentNullException();
            }
        }



        public async Task DeleteApproval(int ApprovalId)
        {
            var Approval = await _context.Approvals.FirstOrDefaultAsync(a => a.Id == ApprovalId); ;
            Approval.Status = ApprovalStatus.Deleted; //Performing SafeDelete
                                                      ;
        }

        public  List<ApprovalModel> GetAllApprovals()
        {
            return  _context.Approvals.Include(s => s.Services).Include(d => d.Diagnoses).Include(a => a.Attachments).ToList();
        }

        public async Task<ApprovalModel> GetApprovalById(int Id)
        {
            var Approval = await _context.Approvals.Include(s => s.Services).Include(d => d.Diagnoses).Include(a => a.Attachments).FirstOrDefaultAsync(a => a.Id == Id); 
            return Approval;
        }

        public async Task UpdateApproval(int ApprovalId, Approvaldto approvaldto)
        {
            var Approval = await _context.Approvals.FirstOrDefaultAsync(a => a.Id == ApprovalId);

            if (approvaldto == null)
            {
                throw new ArgumentNullException(nameof(approvaldto), "Model Not Found");
            }

            DateTime dateTime = DateTime.Now;

            string dateOnly = dateTime.ToString("yyyy-MM-dd");  // Format the datetime as "YYYY-MM-DD"

            TimeSpan timeOnly = dateTime.TimeOfDay;

            if (approvaldto.Attachments != null)
            {
                var newImages = new List<AttachmentModel>();
                var Service = new ServiceModel()
                {
                    Name = approvaldto.ServicesName,
                    Quantiity = approvaldto.ServicesQuantitty,
                };

                var Diagnosis = new DiagonsisModel()
                {

                    Name = approvaldto.DiagnoseName
                };
                foreach (var newImage in approvaldto.Attachments)
                {
                    using var stream = new MemoryStream();
                    await newImage.CopyToAsync(stream);
                    var imageBytes = stream.ToArray();
                    var image = new AttachmentModel
                    {
                        Images = imageBytes
                    };

                    newImages.Add(image);
                }
                var ServiceList = new List<ServiceModel>()
                {
                    Service
                };

                var DiagnosesList = new List<DiagonsisModel>() { Diagnosis };



                Approval.Services = ServiceList;
                Approval.Diagnoses = DiagnosesList;
                Approval.Status = approvaldto.Status;
                Approval.Attachments = newImages
                    ;
                await _context.SaveChangesAsync();


            }
            else
            {
                throw new ArgumentNullException();
            }
        }
    }
}
