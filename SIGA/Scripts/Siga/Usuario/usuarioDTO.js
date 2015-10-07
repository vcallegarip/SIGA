var Request = function (data) {
    this.RequestId = data.RequestId || '';
    this.RequestGuid = data.RequestGuid || '';
    this.SequenceNumber = data.SequenceNumber || '';
    this.RequestName = data.RequestName || '';
    this.Narrative = ko.observable(data.Narrative || '');
    this.SummaryStatusId = ko.observable(data.SummaryStatusId || '');
    this.SummaryStatusText = ko.observable(data.SummaryStatusText || '');
    this.IsDeleted = data.IsDeleted || '';
    this.IsClosed = data.IsClosed || '';
    this.IsApproved = data.IsApproved || '';
    this.BusinessUnitId = data.BusinessUnitId || '';
    this.Summary_ModuleCount = data.Summary_ModuleCount || '';
    this.Summary_ModuleCountClosed = data.Summary_ModuleCountClosed || '';
    this.Summary_ModuleItemCount = data.Summary_ModuleItemCount || '';
    this.Summary_ModuleToApproveCount = data.Summary_ModuleToApproveCount || '';
    this.Summary_ModuleApprovedCount = data.Summary_ModuleApprovedCount || '';
    this.MappedRequestEvents = typeof (data.MappedRequestEvents) == 'undefined' ? [] : $.map(data.MappedRequestEvents, function (item) { return new MappedRequestEvent(item) });
    this.MappedRequestModules = typeof (data.MappedRequestModules) == 'undefined' ? [] : $.map(data.MappedRequestModules, function (item) { return new MappedRequestModule(item) });
    this.PackageTeamPlanDetailID = data.PackageTeamPlanDetailID;
    this.PackageTeamPlanID = data.PackageTeamPlanID;
    this.PackageSourceTypeCode = data.PackageSourceTypeCode;
    this.PackageSourceTypeID = data.PackageSourceTypeID;
    this.PackageID = data.PackageID;
    this.IsSubmitter = data.IsSubmitter;
}