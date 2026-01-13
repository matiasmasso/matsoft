Imports Newtonsoft.Json.Linq

Public Class JsonLog

    Shared Function Find(oGuid As Guid) As DTOJsonLog
        Return JsonLogLoader.Find(oGuid)
    End Function

    Shared Function Update(oJsonLog As DTOJsonLog, exs As List(Of Exception)) As Boolean
        Return JsonLogLoader.Update(oJsonLog, exs)
    End Function

    Shared Function Delete(oJsonLog As DTOJsonLog, exs As List(Of Exception)) As Boolean
        Return JsonLogLoader.Delete(oJsonLog, exs)
    End Function

    Shared Async Function Procesa(exs As List(Of Exception), oJObject As JObject, oEmp As DTOEmp) As Task(Of Boolean)
        Dim oSchema = ReadSchema(oJObject)
        Dim oJsonLog = DTOJsonLog.Factory(oSchema, oJObject.ToString)
        If BEBL.JsonLog.Update(oJsonLog, exs) Then
            If oSchema IsNot Nothing Then

                If oSchema.Equals(DTOJsonSchema.Wellknown(DTOJsonSchema.Wellknowns.Shipments)) Then
                    Dim oShipmentsReport As DTO.Integracions.Vivace.ShipmentsReport = oJObject.ToObject(Of DTO.Integracions.Vivace.ShipmentsReport)
                    oShipmentsReport.Log = oJsonLog

                    If BEBL.ShipmentsReport.Update(exs, oShipmentsReport) Then
                        oJsonLog.Result = DTOJsonLog.Results.Success
                        Dim oDeliveries = BEBL.ShipmentsReport.Deliveries(oJsonLog)
                        BEBL.ShipmentsReport.Procesa(exs, oShipmentsReport, oEmp, oDeliveries)
                    Else
                        oJsonLog.Result = DTOJsonLog.Results.Failure
                    End If
                    BEBL.JsonLog.Update(oJsonLog, exs)
                End If

                If oSchema.Equals(DTOJsonSchema.Wellknown(DTOJsonSchema.Wellknowns.Tracking)) Then
                    Dim oTrackingsReport As DTO.Integracions.Vivace.DeliveryTracking = oJObject.ToObject(Of DTO.Integracions.Vivace.DeliveryTracking)
                    oTrackingsReport.Log = oJsonLog
                    If oTrackingsReport.shipments.Any(Function(X) String.IsNullOrEmpty(X.delivery)) Then
                        oJsonLog.Result = DTOJsonLog.Results.Failure
                        exs.Add(New Exception("falta el número de albaràn"))
                    Else
                        If DAL.DeliveryTrackingLoader.Update(exs, oTrackingsReport) Then
                            oJsonLog.Result = DTOJsonLog.Results.Success
                        Else
                            oJsonLog.Result = DTOJsonLog.Results.Failure
                        End If
                    End If
                    BEBL.JsonLog.Update(oJsonLog, exs)
                End If

                If oSchema.Equals(DTOJsonSchema.Wellknown(DTOJsonSchema.Wellknowns.PurchaseOrder)) Then
                    Dim oPurchaseOrder As DTOCompactPO = oJObject.ToObject(Of DTOCompactPO)
                    Dim value As DTOCompactPO = Await BEBL.PurchaseOrder.SubmitProcess(oPurchaseOrder, pruebas:=False)
                    If value.ValidationErrors.Count = 0 Then
                        oJsonLog.Result = DTOJsonLog.Results.Success
                        oJsonLog.ResultTarget = New DTOBaseGuid(oPurchaseOrder.Guid)
                    Else
                        oJsonLog.Result = DTOJsonLog.Results.Failure
                        For Each ex2 In value.ValidationErrors
                            exs.Add(New Exception(ex2))
                        Next
                    End If
                    BEBL.JsonLog.Update(oJsonLog, exs)
                End If

            End If
        End If

        If exs.Count > 0 Then
            Dim unused = MailMessageHelper.MailAdmin("JsonLog error", ExceptionsHelper.ToHtml(exs) & "<br/><br/>" & oJObject.ToString, exs)
        End If

        Return exs.Count = 0
    End Function

    Shared Function ReadSchema(oJObject As JObject) As DTOJsonSchema
        Dim schema As String = oJObject.SelectToken("schema")
        Dim oSchemaId As DTOJsonSchema.Wellknowns = DTOJsonSchema.Wellknowns.NotSet
        [Enum].TryParse(Of DTOJsonSchema.Wellknowns)(schema, True, oSchemaId)
        Dim retval = DTOJsonSchema.Wellknown(oSchemaId)
        Return retval
    End Function


End Class

Public Class JsonLogs

    Shared Function All(Optional searchkey As String = "") As List(Of DTOJsonLog)
        Return JsonLogsLoader.All(, False, searchkey)
    End Function



End Class
