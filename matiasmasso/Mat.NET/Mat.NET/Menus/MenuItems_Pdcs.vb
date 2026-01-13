
Imports Microsoft.Office.Interop

Public Class MenuItems_Pdcs
    Inherits Windows.forms.MenuItem.MenuItemCollection
    Public Event AfterDelete()

    Private mPdcs As Pdcs

    Public Sub New(ByVal oPdcs As Pdcs)
        MyBase.new(New MenuItem)
        mPdcs = oPdcs
        AddItms()
    End Sub

    Public Sub New(ByVal oPdc As Pdc)
        MyBase.new(New MenuItem)
        mPdcs = New Pdcs
        mPdcs.Add(oPdc)
        AddItms()
    End Sub

    Private Sub AddItms()
        Dim oZoomItm As MenuItem = Add("Zoom", New System.EventHandler(AddressOf Zoom))
        If mPdcs.Count > 1 Then oZoomItm.Enabled = False

        Add("Imprimir", New System.EventHandler(AddressOf Print))
        Add("Pdf", New System.EventHandler(AddressOf PdcPdfBrowse))
        Add("Guardar", New System.EventHandler(AddressOf PdcPdfSave))
        Add("e-mail", New System.EventHandler(AddressOf PdcEmail))

        Dim oMenuDel As MenuItem = Add("Eliminar", New System.EventHandler(AddressOf Del))
        oMenuDel.Enabled = mPdcs.AllowDelete

        With Add("proforma").MenuItems
            .Add("Imprimir", New System.EventHandler(AddressOf ProformaPrint))
            .Add("Pdf", New System.EventHandler(AddressOf ProformaPdfBrowse))
            Add("Guardar", New System.EventHandler(AddressOf ProformaPdfSave))
            .Add("e-mail", New System.EventHandler(AddressOf ProformaEmail))
        End With

        Dim oMenuAlb As New MenuItem("albará", New System.EventHandler(AddressOf NewAlb))
        oMenuAlb.Enabled = False
        If mPdcs.Count = 1 Then
            If mPdcs(0).Client.ExistPncs Then
                oMenuAlb.Enabled = True
            End If
        End If
        Add(oMenuAlb)
    End Sub

    Private Sub Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        root.ShowPdc(mPdcs(0))
    End Sub

    Private Sub Print(ByVal sender As Object, ByVal e As System.EventArgs)
        root.PrintPdcs(mPdcs)
    End Sub

    Private Sub ProformaPrint(ByVal sender As Object, ByVal e As System.EventArgs)
        root.PrintProforma(mPdcs)
    End Sub

    Private Sub Del(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem " & mPdcs.ToString & "?", MsgBoxStyle.OkCancel, "M+O")
        If rc = MsgBoxResult.Ok Then
            Dim exs as New List(Of exception)
            If mPdcs.Delete( exs) Then
                MsgBox("Comandes eliminades", MsgBoxStyle.Information, "M+O")
                RaiseEvent AfterDelete()
            Else
                MsgBox("error al eliminar les comandes" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub ProformaPdfBrowse(ByVal sender As Object, ByVal e As System.EventArgs)
        PdfBrowse(True)
    End Sub

    Private Sub ProformaPdfSave(ByVal sender As Object, ByVal e As System.EventArgs)
        PdfSave(True)
    End Sub

    Private Sub PdcPdfBrowse(ByVal sender As Object, ByVal e As System.EventArgs)
        PdfBrowse(False)
    End Sub

    Private Sub PdcPdfSave(ByVal sender As Object, ByVal e As System.EventArgs)
        PdfSave(False)
    End Sub


    Private Sub PdfBrowse(ByVal BlProforma As Boolean)
        root.ShowPdf(mPdcs.PdfStream(False, BlProforma))
    End Sub

    Private Sub PdfSave(ByVal BlProforma As Boolean)
        Dim sFolder As String = Environment.GetFolderPath(Environment.SpecialFolder.Personal)
        Dim sFileName As String = "M+O " & mPdcs.ToString & ".pdf"

        Dim oDlg As New Windows.Forms.SaveFileDialog
        With oDlg
            .Title = "GUARDAR COMANDES"
            .FileName = sFileName
            .Filter = "acrobat reader(*.pdf)|*.pdf"
            .FilterIndex = 1
            .DefaultExt = "pdf"
            .InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal)
            If .ShowDialog() = DialogResult.OK Then
                mPdcs.Pdf(.FileName)
            End If
        End With
    End Sub

    Private Sub ProformaEmail(ByVal sender As Object, ByVal e As System.EventArgs)
        eMail(True)
    End Sub

    Private Sub PdcEmail(ByVal sender As Object, ByVal e As System.EventArgs)
        eMail(False)
    End Sub

    Private Sub NewAlb(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oClient As Client = mPdcs(0).Client
        root.NewCliAlbNew(oClient)
    End Sub

    Private Sub eMail(ByVal BlProforma As Boolean)
        Dim sSubject As String = mPdcs.ToString(BlProforma).ToUpper
        Dim sDisplayName As String = "M+O " & sSubject & ".pdf"
        Dim sFileName As String = Environment.GetFolderPath(Environment.SpecialFolder.Personal) & "\" & sDisplayName
        mPdcs.Pdf(sFileName, BlProforma)

        Dim oOlApp As New Outlook.Application
        Dim oNewMail As Outlook.MailItem = oOlApp.CreateItem(Outlook.OlItemType.olMailItem)
        With oNewMail
            If mPdcs.SameClient Then
                If Not mPdcs(0).Client.Email Is Nothing Then
                    .Recipients.Add(mPdcs(0).Client.Email)
                End If
            End If
            .Subject = sSubject
            .Attachments.Add(sFileName, , , sDisplayName)
            .Display()
        End With
    End Sub
End Class
