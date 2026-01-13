Public Class Frm_ImportacioItem
    Private _Importacio As Importacio
    Private _DocFile As DTODocFile

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Enum DocTypes
        Fra
        Cmr
    End Enum

    Public Sub New(oImportacio As Importacio, oDocFile As DTODocFile)
        MyBase.New()
        Me.InitializeComponent()
        _Importacio = oImportacio
        _DocFile = oDocFile
        Me.Text = "importar document a la remesa " & _Importacio.Id
        Xl_DocFile1.Load(_DocFile)

        With CheckedListBoxDocSrc
            .Items.Add("Factura comercial")
            .Items.Add("CMR")
        End With

        ButtonSavePdf.Enabled = False
    End Sub

    Private Sub CheckedListBoxDocSrc_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles CheckedListBoxDocSrc.ItemCheck
        Select Case e.Index
            Case DocTypes.Fra
                If e.NewValue Then
                    TextBoxDescripcio.Visible = False
                    LabelDescripcio.Visible = False
                End If
            Case DocTypes.Cmr
                If e.NewValue Then
                    TextBoxDescripcio.Visible = True
                    LabelDescripcio.Visible = True
                End If
        End Select

        ButtonSavePdf.Enabled = e.NewValue

    End Sub

    Private Sub ButtonSavePdf_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonSavePdf.Click
        Dim exs as New List(Of exception)

        Select Case CheckedListBoxDocSrc.CheckedItems(0)
            Case "Factura comercial"
                Dim oFrm As New Wz_Proveidor_NewFra(_Importacio, _DocFile)
                AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                oFrm.Show()
            Case "CMR"
                Dim oItem As New ImportacioItem(_Importacio, ImportacioItem.SourceCodes.Cmr, System.Guid.NewGuid)
                oItem.DocFile = _DocFile
                oItem.Descripcio = TextBoxDescripcio.Text
                _Importacio.Items.Add(oItem)

                If BLL.BLLDocFile.Update(_DocFile, DTODocFile.Cods.Cmr, oItem.Guid, exs) Then
                    If _Importacio.UpdateItems(exs) Then
                        RefreshRequest(Me, New MatEventArgs(oItem))
                    Else
                        MsgBox("error al importar el CMR a la remesa" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
                    End If
                Else
                    MsgBox("error al importar el pdf del CMR a la remesa" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
                End If
        End Select
    End Sub

    Private Sub RefreshRequest(sender As Object, e As MatEventArgs)
        RaiseEvent AfterUpdate(Me, e)
        Me.Close()
    End Sub
End Class