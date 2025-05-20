Imports Microsoft.VisualBasic

Public Class clsMessage
    Enum EnumMessageType
        DeniedSave = 1
        SuccessSave = 2
        DeniedEdit = 3
        SuccessEdit = 4
        DeniedDelete = 5
        SuccessDelete = 6
        DeniedView = 7
        SuccessView = 8
        DeniedPost = 9
        SuccesPost = 10
        DeniedAdd = 11
        SuccessAdd = 12
        AllowDateRange = 13
        DuplicateRecord = 14
        ErrorSave = 15
        DeniedApproveDisApproved = 16
        NoSelectedRecordToPost = 17
        DeniedProcess = 18
        NoSelectedTransaction = 19
        SuccessfullyTagToFailed = 20
        UnableToCreateAlreadyInUsed = 21
        ContinuosPooling = 22
        ItemFilingUp = 23
        requiredData = 24
        submitForApproval = 25

    End Enum

    Public Function clsMessages(ByVal pIndex As Integer) As String
        Select Case pIndex
            Case 1 : Return "Posted Record cannot be recovered."
            Case Else
                Return ""
        End Select
    End Function
    Public Function GetMessageType(ByVal bPermissionType As EnumMessageType) As String
        Dim bRetVal As String
        bRetVal = "Please secure permission from your team leader or administrator."
        Select Case bPermissionType
            Case EnumMessageType.DeniedDelete : bRetVal = "Access Denied! Unable to delete record."
            Case EnumMessageType.SuccessDelete : bRetVal = "Record(s) has been successfully deleted."
            Case EnumMessageType.DeniedAdd : bRetVal = "Access Denied! Unable to Add record."
            Case EnumMessageType.SuccessAdd : bRetVal = "Record has been successfully created."
            Case EnumMessageType.DeniedEdit : bRetVal = "Access Denied! Unable to modify record."
            Case EnumMessageType.SuccessEdit : bRetVal = "Record has been successfully modified."
            Case EnumMessageType.DeniedPost : bRetVal = "Access Denied! Posted record cannot be modified."
            Case EnumMessageType.SuccesPost : bRetVal = "Record has been successfully posted."
            Case EnumMessageType.DeniedSave : bRetVal = "Access Denied! Unable to save record."
            Case EnumMessageType.SuccessSave : bRetVal = "Record has been successfully saved."
            Case EnumMessageType.AllowDateRange : bRetVal = "Startdate should be less than to end date."
            Case EnumMessageType.DuplicateRecord : bRetVal = "Duplicate Record."
            Case EnumMessageType.ErrorSave : bRetVal = "Error saving of Record."
            Case EnumMessageType.DeniedApproveDisApproved : bRetVal = "Approved or Disapproved transaction cannot be edited!"
            Case EnumMessageType.NoSelectedRecordToPost : bRetVal = "No selected record for posting!"
            Case EnumMessageType.DeniedProcess : bRetVal = "No access permission to process this transaction."
            Case EnumMessageType.NoSelectedTransaction : bRetVal = "No selected  record."
            Case EnumMessageType.SuccessfullyTagToFailed : bRetVal = "record(s) has been successfully tag to Failed."
            Case EnumMessageType.UnableToCreateAlreadyInUsed : bRetVal = "Unable to create this transaction, Item no already in used.!"
            Case EnumMessageType.ContinuosPooling : bRetVal = "Continuos Pooling must uncheck if Item no. selected"
            Case EnumMessageType.ItemFilingUp : bRetVal = "Item no. with incumbent/tagged for filing-up"
            Case EnumMessageType.requiredData : bRetVal = "Data for this module is required."
            Case EnumMessageType.submitForApproval : bRetVal = "Successfully submitted for approval."
        End Select
        Return bRetVal
    End Function

End Class

