Namespace Integracions.Edi
    Public Class Invrpt

        Shared Function Procesa(exs As List(Of Exception), oFile As DTOEdiversaFile) As Boolean
            Dim oEmp As New DTOEmp(DTOEmp.Ids.MatiasMasso)
            Dim oGlns As Dictionary(Of String, Guid) = ContactsLoader.Glns(oEmp)
            Dim oSkuIds As List(Of DTOProductSkuId) = ProductSkusLoader.Ids(oEmp)
            Dim oInvRpt = DTO.Integracions.Edi.Invrpt.Factory(oFile)
            loadInterlocutors(oInvRpt, oGlns)
            loadSkus(oInvRpt, oSkuIds)
            Dim retval = DAL.Integracions.Edi.InvRptLoader.Update(exs, oInvRpt)
            Return retval
        End Function

        Shared Function Raport(customer As Guid, sku As Guid, fch As DateTime) As String
            Dim retval = DAL.Integracions.Edi.InvRptLoader.Raport(customer, sku, fch)
            Return retval
        End Function

        Shared Function Delete(exs As List(Of Exception), oInvRpt As DTO.Integracions.Edi.Invrpt)
            Return DAL.Integracions.Edi.InvRptLoader.Delete(exs, oInvRpt)
        End Function

        Shared Sub loadInterlocutors(oInvrpt As DTO.Integracions.Edi.Invrpt, oGlns As Dictionary(Of String, Guid))
            For Each oInterlocutor In oInvrpt.Interlocutors
                Dim oGuid As Guid = Nothing
                If oGlns.TryGetValue(oInterlocutor.Gln, oGuid) Then
                    oInterlocutor.Guid = oGuid
                Else
                    Select Case oInterlocutor.Qualifier
                        Case DTO.Integracions.Edi.Invrpt.Interlocutor.Qualifiers.Sender
                            oInvrpt.AddException(DTO.Integracions.Edi.Invrpt.ExceptionCods.senderNotFound, String.Format("No s'ha trobat l'interlocutor {0} amb Gln {1} {2}", oInterlocutor.Qualifier.ToString, oInterlocutor.Gln, oInterlocutor.Nom), DTOEdiversaException.TagCods.Gln, oInterlocutor.Gln)
                        Case DTO.Integracions.Edi.Invrpt.Interlocutor.Qualifiers.Receiver
                            oInvrpt.AddException(DTO.Integracions.Edi.Invrpt.ExceptionCods.receiverNotFound, String.Format("No s'ha trobat l'interlocutor {0} amb Gln {1} {2}", oInterlocutor.Qualifier.ToString, oInterlocutor.Gln, oInterlocutor.Nom), DTOEdiversaException.TagCods.Gln, oInterlocutor.Gln)
                        Case DTO.Integracions.Edi.Invrpt.Interlocutor.Qualifiers.Reporting
                            oInvrpt.AddException(DTO.Integracions.Edi.Invrpt.ExceptionCods.reportingNotFound, String.Format("No s'ha trobat l'interlocutor {0} amb Gln {1} {2}", oInterlocutor.Qualifier.ToString, oInterlocutor.Gln, oInterlocutor.Nom), DTOEdiversaException.TagCods.Gln, oInterlocutor.Gln)
                    End Select
                End If
            Next
        End Sub

        Shared Sub loadSkus(oInvrpt As DTO.Integracions.Edi.Invrpt, oSkuIds As List(Of DTOProductSkuId))
            For Each item In oInvrpt.Items
                Dim oGuid = SkuGuid(item, oSkuIds)
                If oGuid Is Nothing Then
                    Dim refs = String.Join("-", item.SkuIds.Select(Function(x) x.Id))
                    item.AddException(DTO.Integracions.Edi.Invrpt.ExceptionCods.skuNotFound, String.Format("No s'ha trobat cap article per {0}", refs))
                Else
                    item.AddSkuId(DTO.Integracions.Edi.Invrpt.SkuId.Qualifiers.Guid, oGuid.ToString())
                End If
            Next

            For i = oInvrpt.Items.Count - 1 To 0 Step -1
                Dim iSkuId = oInvrpt.Items(i).SkuIds.FirstOrDefault(Function(x) x.Qualifier = DTO.Integracions.Edi.Invrpt.SkuId.Qualifiers.Guid)
                If iSkuId IsNot Nothing Then
                    For j = i - 1 To 0 Step -1
                        Dim jSkuId = oInvrpt.Items(j).SkuIds.FirstOrDefault(Function(x) x.Qualifier = DTO.Integracions.Edi.Invrpt.SkuId.Qualifiers.Guid)
                        If jSkuId IsNot Nothing Then
                            If iSkuId.Id = jSkuId.Id Then
                                oInvrpt.Items(j).Qty += oInvrpt.Items(i).Qty
                                oInvrpt.Items.RemoveAt(i)
                                Exit For
                            End If
                        End If
                    Next
                End If
            Next
        End Sub

        Shared Function SkuGuid(item As DTO.Integracions.Edi.Invrpt.Item, oSkuIds As List(Of DTOProductSkuId)) As Guid?
            Dim retval As Guid? = Nothing

            'search by Ean
            Dim skuEan = item.SkuIds.FirstOrDefault(Function(x) x.Qualifier = DTO.Integracions.Edi.Invrpt.SkuId.Qualifiers.Ean)
            If skuEan IsNot Nothing Then
                If Not String.IsNullOrEmpty(skuEan.Id) AndAlso skuEan.Id <> "0000000000000" Then
                    Dim skuId = oSkuIds.FirstOrDefault(Function(x) x.Ean = skuEan.Id)
                    If skuId IsNot Nothing Then
                        retval = skuId.Guid
                    End If
                End If
            End If

            'search by supplier Id
            If retval Is Nothing Then
                For Each sku In item.SkuIds.Where(Function(x) x.Qualifier = DTO.Integracions.Edi.Invrpt.SkuId.Qualifiers.Supplier).ToList()
                    Dim cleanRef = sku.Id.Split(" ")(0)
                    Dim oSkuId As DTOProductSkuId = Nothing
                    If IsNumeric(cleanRef) Then
                        oSkuId = oSkuIds.FirstOrDefault(Function(x) x.Id = cleanRef)
                    End If

                    If oSkuId Is Nothing Then oSkuId = oSkuIds.FirstOrDefault(Function(x) x.Ref = cleanRef)
                    If oSkuId IsNot Nothing Then
                        retval = oSkuId.Guid
                        Exit For
                    End If
                Next
            End If

            Return retval
        End Function

    End Class

    Public Class Invrpts
        Shared Function ProcesaOpenFiles(exs As List(Of Exception)) As Boolean
            Dim oEmp As New DTOEmp(DTOEmp.Ids.MatiasMasso)
            Dim oFiles = EdiversaFilesLoader.All("INVRPT_D_96A_UN_EAN004", True)
            Dim oInvRpts As New List(Of DTO.Integracions.Edi.Invrpt)
            Dim oGlns As Dictionary(Of String, Guid) = ContactsLoader.Glns(oEmp)
            Dim oSkuIds As List(Of DTOProductSkuId) = ProductSkusLoader.Ids(oEmp)
            For Each oFile In oFiles
                Dim oInvRpt = DTO.Integracions.Edi.Invrpt.Factory(oFile)
                Invrpt.loadInterlocutors(oInvRpt, oGlns)
                Invrpt.loadSkus(oInvRpt, oSkuIds)
                oInvRpts.Add(oInvRpt)
            Next

            Dim retval = DAL.Integracions.Edi.InvRptsLoader.Update(exs, oInvRpts)
            Return retval
        End Function


        Shared Function Model(oHolding As DTOHolding, oUser As DTOUser, fch As Nullable(Of Date)) As DTO.Models.InvrptModel
            Return DAL.Integracions.Edi.InvRptsLoader.Model(oHolding, oUser, fch)
        End Function

        Shared Function Exceptions() As List(Of DTO.Integracions.Edi.Exception)
            Return DAL.Integracions.Edi.InvRptsLoader.Exceptions()
        End Function
    End Class
End Namespace

