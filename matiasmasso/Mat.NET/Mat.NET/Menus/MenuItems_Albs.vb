
Imports Microsoft.Office.Interop

Public Class MenuItems_Albs
    Inherits Windows.forms.MenuItem.MenuItemCollection
    Public Event AfterDelete()

    Private mAlbs As Albs

    Public Sub New(ByVal oAlbs As Albs)
        MyBase.new(New MenuItem)
        mAlbs = oAlbs
        AddItms()
    End Sub

    Public Sub New(ByVal oAlb As Alb)
        MyBase.new(New MenuItem)
        mAlbs = New Albs
        mAlbs.Add(oAlb)
        AddItms()
    End Sub

    Private Sub AddItms()
        Dim oItm As MenuItem

        oItm = Add("Zoom", New System.EventHandler(AddressOf Zoom))
        oItm.Enabled = (mAlbs.Count = 1)

        Add("Imprimir", New System.EventHandler(AddressOf Print))
        Add("Pdf", New System.EventHandler(AddressOf Pdf))
        Add("e-mail", New System.EventHandler(AddressOf eMail))
        With Add("proforma").MenuItems
            .Add("Print", New System.EventHandler(AddressOf PrintProforma))
            .Add("Pdf", New System.EventHandler(AddressOf PdfProforma))
            .Add("e-mail", New System.EventHandler(AddressOf eMailProforma))
        End With
        If mAlbs.Count = 1 Then
            Add("comparar transp", New System.EventHandler(AddressOf ShowAlbTrps))
        End If

        oItm = Add("Facturar", New System.EventHandler(AddressOf Frx))
        oItm.Enabled = AllowFrx()

        Add("Eliminar", New System.EventHandler(AddressOf Del))
    End Sub

    Private Sub Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        root.ShowAlb(mAlbs(0))
    End Sub

    Private Sub Print(ByVal sender As Object, ByVal e As System.EventArgs)
        root.PrintAlbs(mAlbs)
    End Sub

    Private Sub PrintProforma(ByVal sender As Object, ByVal e As System.EventArgs)
        root.PrintProforma(mAlbs)
    End Sub

    Private Sub Frx(ByVal sender As Object, ByVal e As System.EventArgs)
        root.ExeFacturacio(mAlbs)
    End Sub

    Private Sub ShowAlbTrps(ByVal sender As Object, ByVal e As System.EventArgs)
        root.ShowAlbTrps(mAlbs(0))
    End Sub

    Private Function AllowFrx() As Boolean
        Dim retval As Boolean
        For Each oAlb As Alb In mAlbs
            If oAlb.Fra Is Nothing Then
                Return retval
                Exit Function
            ElseIf oAlb.Fra.Id > 0 Then
                Return retval
                Exit Function
            ElseIf oAlb.Facturable = False Then
                Return retval
                Exit Function
            End If
        Next
        retval = True
        Return retval
    End Function

    Private Sub Pdf(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim sFolder As String = Environment.GetFolderPath(Environment.SpecialFolder.Personal)
        Dim sFileName As String = "M+O " & mAlbs.ToString & ".pdf"

        Dim oDlg As New Windows.Forms.SaveFileDialog
        With oDlg
            .Title = "GUARDAR ALBARANS"
            .FileName = sFileName
            .Filter = "acrobat reader(*.pdf)|*.pdf"
            .FilterIndex = 1
            .DefaultExt = "pdf"
            .InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal)
            If .ShowDialog() = DialogResult.OK Then
                mAlbs.Pdf(.FileName)
            End If
        End With
    End Sub

    Private Sub eMail(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim sSubject As String = mAlbs.ToString.ToUpper
        Dim sDisplayName As String = "M+O " & sSubject & ".pdf"
        Dim sFileName As String = Environment.GetFolderPath(Environment.SpecialFolder.Personal) & "\" & sDisplayName
        mAlbs.Pdf(sFileName)

        Dim oOlApp As New Outlook.Application
        Dim oNewMail As Outlook.MailItem = oOlApp.CreateItem(Outlook.OlItemType.olMailItem)
        With oNewMail
            If mAlbs.SameClient Then
                If Not mAlbs(0).Client.Email Is Nothing Then
                    .Recipients.Add(mAlbs(0).Client.Email)
                End If
            End If
            .Subject = sSubject
            .Attachments.Add(sFileName, , , sDisplayName)
            .Display()
        End With
    End Sub

    Private Sub PdfProforma(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim sFolder As String = Environment.GetFolderPath(Environment.SpecialFolder.Personal)
        Dim sFileName As String = "M+O " & mAlbs.ToString(True) & ".pdf"

        Dim oDlg As New Windows.Forms.SaveFileDialog
        With oDlg
            .Title = "GUARDAR PROFORMES"
            .FileName = sFileName
            .Filter = "acrobat reader(*.pdf)|*.pdf"
            .FilterIndex = 1
            .DefaultExt = "pdf"
            .InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal)
            If .ShowDialog() = DialogResult.OK Then
                mAlbs.Pdf(.FileName, True)
            End If
        End With
    End Sub

    Private Sub eMailProforma(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim sSubject As String = mAlbs.Text(True).ToUpper
        Dim sDisplayName As String = "M+O " & sSubject & ".pdf"
        Dim sFileName As String = Environment.GetFolderPath(Environment.SpecialFolder.Personal) & "\" & sDisplayName
        mAlbs.Pdf(True, sFileName)

        Dim oOlApp As New Outlook.Application
        Dim oNewMail As Outlook.MailItem = oOlApp.CreateItem(Outlook.OlItemType.olMailItem)
        With oNewMail
            If mAlbs.SameClient Then
                If Not mAlbs(0).Client.Email Is Nothing Then
                    .Recipients.Add(mAlbs(0).Client.Email)
                End If
            End If
            .Subject = sSubject
            .Attachments.Add(sFileName, , , sDisplayName)
            .Display()
        End With
    End Sub

    Private Sub Del(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("eliminem albarans " & mAlbs.ToString, MsgBoxStyle.OKCancel, "MAT.NET")
        If rc = MsgBoxResult.OK Then
            Dim sRc As String
            sRc = mAlbs.Delete
            If sRc = "" Then
                MsgBox("albarans eliminats", MsgBoxStyle.Information, "MAT.NET")
                RaiseEvent AfterDelete()
            Else
                MsgBox("Els següents albarans no s'han pogut eliminar:" & vbCrLf & sRc, MsgBoxStyle.Exclamation, "MAT.NET")
            End If
        Else
            MsgBox("Operació cancelada per l'usuari", MsgBoxStyle.Exclamation, "MAT.NET")
        End If
    End Sub
End Class

