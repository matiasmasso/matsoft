Public Class EdiversaDesadv


#Region "CRUD"
    Shared Function Find(oGuid As Guid) As DTOEdiversaDesadv
        Dim retval As DTOEdiversaDesadv = EdiversaDesadvLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Load(ByRef oEdiversaDesadv As DTOEdiversaDesadv) As Boolean
        Dim retval As Boolean = EdiversaDesadvLoader.Load(oEdiversaDesadv)
        Return retval
    End Function

    Shared Function Update(oEdiversaDesadv As DTOEdiversaDesadv, exs As List(Of Exception)) As Boolean
        Return EdiversaDesadvLoader.Update(oEdiversaDesadv, exs)
    End Function

    Shared Function Delete(oEdiversaDesadv As DTOEdiversaDesadv, ByRef exs As List(Of Exception)) As Boolean
        Return EdiversaDesadvLoader.Delete(oEdiversaDesadv, exs)
    End Function
#End Region

    Shared Function Factory(oEmp As DTOEmp, ByRef oEdiFile As DTOEdiversaFile, exs As List(Of Exception)) As DTOEdiversaDesadv
        Dim retval As New DTOEdiversaDesadv(oEdiFile)
        Dim item As New DTOEdiversaDesadv.Item
        With retval

            For Each oSegment In oEdiFile.Segments
                Try

                    Select Case oSegment.Fields.First
                        Case "BGM"
                            .Bgm = oSegment.Fields(1)
                        Case "DTM"
                            .FchDoc = DTOEdiversaFile.ParseFch(oSegment.Fields(1), .Exceptions)
                            .FchShip = DTOEdiversaFile.ParseFch(oSegment.Fields(4), .Exceptions)
                        Case "RFF"
                            If oSegment.Fields.Count > 2 Then
                                .Rff = oSegment.Fields(2)
                                If TextHelper.RegexMatch(.Rff, "^[0-9]{4}$") Then
                                    .PurchaseOrder = BEBL.PurchaseOrder.FromRff(oEmp, .Rff)
                                    If .PurchaseOrder Is Nothing Then
                                        .Exceptions.Add(DTOEdiversaException.Factory(DTOEdiversaException.Cods.PurchaseOrderNotFound, Nothing, String.Format("Desadv.{0}: format invalid de comanda '{1}'", .Bgm, .Rff)))
                                    End If
                                Else
                                    .Exceptions.Add(DTOEdiversaException.Factory(DTOEdiversaException.Cods.PurchaseOrderNotFound, Nothing, String.Format("Desadv.{0}: No s'ha trobat cap comanda per '{1}'", .Bgm, .Rff)))
                                End If
                            Else
                                .Exceptions.Add(DTOEdiversaException.Factory(DTOEdiversaException.Cods.MissingSegmentFields, Nothing, "Segment RFF amb menys camps del compte"))
                            End If
                        Case "NADBY"
                            .NadBy = DTOEdiversaFile.ParseEAN(oSegment.Fields(1), .Exceptions)
                        Case "NADSU"
                            .NadSu = DTOEdiversaFile.ParseEAN(oSegment.Fields(1), .Exceptions)
                            .Proveidor = DTOProveidor.FromContact(BEBL.Contact.FromGLN(.NadSu))
                            If .Proveidor Is Nothing Then
                                .Exceptions.Add(DTOEdiversaException.Factory(DTOEdiversaException.Cods.InterlocutorNotFound, Nothing, String.Format("No s'ha trobat cap proveidor per '{0}'", oSegment.Fields(1))))
                            End If
                        Case "NADDP"
                            .NadDp = DTOEdiversaFile.ParseEAN(oSegment.Fields(1), .Exceptions)
                            .Entrega = BEBL.Contact.FromGLN(.NadDp)
                            If .Entrega Is Nothing Then
                                .Exceptions.Add(DTOEdiversaException.Factory(DTOEdiversaException.Cods.InterlocutorNotFound, Nothing, String.Format("No s'ha trobat cap punt d'entrega per '{0}'", oSegment.Fields(1))))
                            End If
                        Case "LIN"
                            item = New DTOEdiversaDesadv.Item()
                            If IsNumeric(oSegment.Fields(1)) Then
                                item.Ean = DTOEdiversaFile.ParseEAN(oSegment.Fields(1), .Exceptions)
                                item.Sku = BEBL.ProductSku.FromEan(item.Ean)
                                If item.Sku Is Nothing Then
                                    .Exceptions.Add(DTOEdiversaException.Factory(DTOEdiversaException.Cods.SkuNotFound, Nothing, String.Format("No s'ha trobat cap producte amb EAN '{0}'", oSegment.Fields(1))))
                                End If
                            Else
                                .Exceptions.Add(DTOEdiversaException.Factory(DTOEdiversaException.Cods.SkuNotFound, Nothing, String.Format("Falta l'EAN a la linia {0}", oSegment.Fields(3))))
                            End If
                            .Items.Add(item)
                        Case "PIALIN"
                            If oSegment.Fields.Count > 2 Then
                                item.Ref = oSegment.Fields(2)
                            Else
                                .Exceptions.Add(DTOEdiversaException.Factory(DTOEdiversaException.Cods.MissingSegmentFields, Nothing, "Segment PIALIN amb menys camps del compte"))
                            End If
                        Case "IMDLIN"
                            If oSegment.Fields.Count > 2 Then
                                item.Dsc = oSegment.Fields(2)
                            Else
                                .Exceptions.Add(DTOEdiversaException.Factory(DTOEdiversaException.Cods.MissingSegmentFields, Nothing, "Segment IMDLIN amb menys camps del compte"))
                            End If
                        Case "QTYLIN"
                            If oSegment.Fields.Count > 2 Then
                                item.Qty = oSegment.Fields(2)
                            Else
                                .Exceptions.Add(DTOEdiversaException.Factory(DTOEdiversaException.Cods.MissingSegmentFields, Nothing, "Segment QTYLIN amb menys camps del compte"))
                            End If
                    End Select
                Catch ex As Exception
                    exs.Add(New Exception(String.Format("segment {0}: {1}", oSegment.ToString, ex.Message)))
                End Try

            Next
        End With

        Return retval
    End Function


End Class

Public Class EdiversaDesadvs

    Shared Function All() As List(Of DTOEdiversaDesadv)
        Dim retval As List(Of DTOEdiversaDesadv) = EdiversaDesadvsLoader.All()
        Return retval
    End Function

    Shared Function SendViaEdi(exs As List(Of Exception), oDeliveries As List(Of DTODelivery), oEmp As DTOEmp) As Boolean
        For Each oDelivery In oDeliveries
            BEBL.Delivery.Load(oDelivery)
            oDelivery.emp = oEmp 'per dades GLN Org
            Dim oDesadv = oDelivery.Desadv(exs)

            Dim oSender As New DTOEdiversaContact
            With oSender
                .Contact = oEmp.Org
                .Ean = .Contact.GLN
            End With

            Dim oReceiver As New DTOEdiversaContact
            With oReceiver
                .Contact = oDelivery.customer
                .Ean = .Contact.GLN
            End With

            Dim oEdiversaFile = New DTOEdiversaFile
            With oEdiversaFile
                .tag = DTOEdiversaFile.Tags.DESADV_D_96A_UN_EAN005.ToString
                .fch = oDelivery.fch
                .sender = oSender
                .receiver = oReceiver
                .docnum = oDelivery.id
                .amount = oDelivery.totalCash
                .result = DTOEdiversaFile.Results.pending
                .resultBaseGuid = oDelivery
                .stream = oDesadv.EdiversaMessage()
                .IOCod = DTOEdiversaFile.IOcods.outbox
            End With

            BEBL.EdiversaFile.Update(oEdiversaFile, exs)
        Next
        Return exs.Count = 0
    End Function

End Class


