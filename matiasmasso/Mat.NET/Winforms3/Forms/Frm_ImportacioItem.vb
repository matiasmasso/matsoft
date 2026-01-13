Public Class Frm_ImportacioItem
    Private _Importacio As DTOImportacio
    Private _DocFile As DTODocFile

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Enum DocTypes
        Fra
        Cmr
    End Enum

    Public Sub New(oImportacio As DTOImportacio, oDocFile As DTODocFile)
        MyBase.New()
        Me.InitializeComponent()
        _Importacio = oImportacio
        _DocFile = oDocFile
        Me.Text = "importar document a la remesa " & _Importacio.Id
        Xl_DocFile1.Load(_DocFile)

        With CheckedListBoxDocSrc
            .DisplayMember = "Key"
            .ValueMember = "Value"
            .Items.Add(New KeyValuePair(Of String, DTOImportacioItem.SourceCodes)("Factura proforma", DTOImportacioItem.SourceCodes.Proforma))
            .Items.Add(New KeyValuePair(Of String, DTOImportacioItem.SourceCodes)("Factura comercial", DTOImportacioItem.SourceCodes.Fra))
            .Items.Add(New KeyValuePair(Of String, DTOImportacioItem.SourceCodes)("Factura transportista", DTOImportacioItem.SourceCodes.FraTrp))
            .Items.Add(New KeyValuePair(Of String, DTOImportacioItem.SourceCodes)("CMR / Bill of Landing", DTOImportacioItem.SourceCodes.Cmr))
            .Items.Add(New KeyValuePair(Of String, DTOImportacioItem.SourceCodes)("Packing List", DTOImportacioItem.SourceCodes.PackingList))
        End With

        ButtonSavePdf.Enabled = False
    End Sub


    Private Sub CheckedListBoxDocSrc_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles CheckedListBoxDocSrc.ItemCheck
        Dim oKeyValuePair As KeyValuePair(Of String, DTOImportacioItem.SourceCodes) = CheckedListBoxDocSrc.Items(e.Index)
        Select Case oKeyValuePair.Value
            Case DTOImportacioItem.SourceCodes.Fra, DTOImportacioItem.SourceCodes.FraTrp
                If e.NewValue Then
                    TextBoxDescripcio.Visible = False
                    LabelDescripcio.Visible = False
                End If
            Case Else
                If e.NewValue Then
                    TextBoxDescripcio.Visible = True
                    LabelDescripcio.Visible = True
                End If
        End Select

        ButtonSavePdf.Enabled = e.NewValue

    End Sub

    Private Async Sub ButtonSavePdf_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonSavePdf.Click
        Dim exs As New List(Of Exception)
        Dim oCheckedItem As KeyValuePair(Of String, DTOImportacioItem.SourceCodes) = CheckedListBoxDocSrc.CheckedItems(0)

        Select Case oCheckedItem.Value
            Case DTOImportacioItem.SourceCodes.Fra, DTOImportacioItem.SourceCodes.FraTrp
                Dim oFrm As New Wz_Proveidor_NewFra(_Importacio, _DocFile)
                AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                oFrm.Show()
            Case Else
                'Dim oItem As New ImportacioItem(_Importacio, DTOImportacioItem.SourceCodes.Cmr, System.Guid.NewGuid)
                Dim oItem = DTOImportacioItem.Factory(_Importacio, oCheckedItem.Value)
                oItem.DocFile = _DocFile
                oItem.Descripcio = TextBoxDescripcio.Text

                If FEB.Importacio.Load(exs, _Importacio) Then
                    _Importacio.Items.Add(oItem)

                    If Await FEB.DocFile.Upload(_DocFile, exs) Then
                        Dim id = Await FEB.Importacio.Update(_Importacio, exs)
                        If exs.Count = 0 Then
                            RefreshRequest(Me, New MatEventArgs(oItem))
                        Else
                            MsgBox("error al importar el document a la remesa" & vbCrLf & ExceptionsHelper.ToFlatString(exs), MsgBoxStyle.Exclamation)
                        End If
                    Else
                        MsgBox("error al importar el pdf del document a la remesa" & vbCrLf & ExceptionsHelper.ToFlatString(exs), MsgBoxStyle.Exclamation)
                    End If
                Else
                    UIHelper.WarnError(exs)
                End If
        End Select
    End Sub


    Private Sub RefreshRequest(sender As Object, e As MatEventArgs)
        RaiseEvent AfterUpdate(Me, e)
        Me.Close()
    End Sub
End Class