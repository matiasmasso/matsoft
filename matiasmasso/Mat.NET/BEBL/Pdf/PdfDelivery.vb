Public Class PdfDelivery
    Inherits LegacyHelper.PdfTemplateForm

    Private _Deliveries As List(Of DTODelivery)
    Private _Proforma As Boolean
    Private _Valoracio As DTODelivery.CodsValorat = DTODelivery.CodsValorat.Inherit

    Public Sub New(oDeliveries As List(Of DTODelivery), Optional oProforma As Boolean = False)
        MyBase.New(DTOLang.ESP)
        _Deliveries = oDeliveries
        _Proforma = oProforma
    End Sub

    Public Sub Print(oValoracio As DTODelivery.CodsValorat, exs As List(Of Exception))
        Dim oDoc As DTODoc
        Dim oDelivery As DTODelivery
        Dim BlPrintEciHeader As Boolean

        _Valoracio = oValoracio

        For i As Integer = 0 To _Deliveries.Count - 1

            oDelivery = _Deliveries(i)

            Try
                BEBL.Delivery.Load(oDelivery)
                Select Case oDelivery.exportCod()
                    Case DTOInvoice.ExportCods.extracomunitari
                        oDelivery.valorado = True
                    Case Else
                        Select Case oValoracio
                            Case DTODelivery.CodsValorat.ForceTrue
                                oDelivery.valorado = True
                            Case DTODelivery.CodsValorat.ForceFalse
                                oDelivery.Valorado = False
                            Case Else
                                If oDelivery.Customer IsNot Nothing Then
                                    oDelivery.Valorado = oDelivery.Customer.AlbValorat
                                Else
                                    oDelivery.Valorado = True
                                End If
                        End Select
                End Select

                Dim oContact As DTOContact = oDelivery.contact
                'FEBL.Contact.Load(oContact)

                Select Case oDelivery.cod
                    Case DTOPurchaseOrder.Codis.proveidor
                        SetDetailTabWidths()
                    Case Else
                        Dim oCustomer = oDelivery.customer
                        SetDetailTabWidths(BlMostrarEAN:=oDelivery.customer.mostrarEANenFactura)
                        MyBase.LabelTachado = oDelivery.customerDocURL > ""
                        MyBase.LabelExport = False ' oDelivery.exportCod = DTOInvoice.ExportCods.extracomunitari

                        'check portada ECI
                        Dim HasEciHeader As Boolean = BEBL.ElCorteIngles.Belongs(oCustomer) And oDelivery.Platform IsNot Nothing
                        BlPrintEciHeader = HasEciHeader
                        If BlPrintEciHeader AndAlso i > 0 Then
                            If oDelivery.hasSameAddressOf(_Deliveries(i - 1)) Then
                                BlPrintEciHeader = False
                            End If
                        End If

                        If BlPrintEciHeader Then
                            Dim oEciDeliveries As New List(Of DTODelivery)
                            oEciDeliveries.Add(oDelivery)
                            For j As Integer = i + 1 To _Deliveries.Count - 1
                                If oDelivery.hasSameAddressOf(_Deliveries(j)) Then
                                    oEciDeliveries.Add(_Deliveries(j))
                                Else
                                    Exit For
                                End If
                            Next


                            oDoc = BEBL.ElCorteIngles.AlbHeaderDoc(oEciDeliveries)
                            MyBase.PrintDoc(oDoc)
                        End If

                        'check plataforma de entrega
                        Dim oPlataforma = oDelivery.Platform 'Valid per elcas de Gibraltar Veen.. 
                        If oPlataforma Is Nothing Then oPlataforma = oCustomer.DeliveryPlatform 'valid per MiFarma
                        If HasEciHeader Then oPlataforma = Nothing

                        If oPlataforma IsNot Nothing Then
                            BEBL.Contact.Load(oPlataforma)


                            Dim oPlatfAlbs As New List(Of DTODelivery)
                            oPlatfAlbs.Add(oDelivery)
                            For j As Integer = i + 1 To _Deliveries.Count - 1
                                If oDelivery.Customer.DeliveryPlatform IsNot Nothing AndAlso oDelivery.Customer.DeliveryPlatform.Equals(_Deliveries(j).Customer.DeliveryPlatform) Then
                                    oPlatfAlbs.Add(_Deliveries(j))
                                End If
                            Next
                            oDoc = PlataformaPage(oPlataforma, oPlatfAlbs)
                            PrintDoc(oDoc, 2)

                        End If

                End Select

                oDoc = BEBL.Delivery.Doc(oDelivery, _Proforma)
                PrintDoc(oDoc)
            Catch ex As Exception
                exs.Add(New Exception("Error al generar el Pdf de l'albarà " & oDelivery.id))
                exs.Add(ex)
            End Try
        Next
    End Sub


End Class
