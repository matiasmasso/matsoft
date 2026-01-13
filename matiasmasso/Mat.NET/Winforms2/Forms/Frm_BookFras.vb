Imports System.IO
Imports System.IO.Compression

Public Class Frm_BookFras
    Private _values As List(Of DTOBookFra)

    Public Sub New(Optional values As List(Of DTOBookFra) = Nothing)
        MyBase.New
        InitializeComponent()
        _values = values
    End Sub

    Private Async Sub Frm_BookFras_Load(sender As Object, e As EventArgs) Handles Me.Load
        If _values Is Nothing Then
            Dim iYears As New List(Of Integer)
            For i As Integer = DTO.GlobalVariables.Today().Year To 1985 Step -1
                iYears.Add(i)
            Next
            Xl_Years1.Load(iYears)
            Await refresca()
        Else
            Xl_Years1.Visible = False
            Xl_BookFras1.Load(_values)

            If _values IsNot Nothing AndAlso _values.Count > 0 Then
                Dim oFirstContact = _values.First.Contact
                If _values.All(Function(x) x.Contact.Equals(oFirstContact)) Then
                    Me.Text += " " & oFirstContact.FullNom
                End If
            End If

            ProgressBar1.Visible = False
        End If

    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Dim year As Integer = Xl_Years1.Value
        Dim oExercici As New DTOExercici(Current.Session.Emp, year)

        ProgressBar1.Visible = True
        Application.DoEvents()
        _values = Await FEB.Bookfras.All(exs, DTOBookFra.Modes.all, oExercici)
        ProgressBar1.Visible = False

        If exs.Count = 0 Then
            Xl_BookFras1.Load(_values)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Sub Xl_TextboxSearch1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_TextboxSearch1.AfterUpdate
        Xl_BookFras1.Filter = e.Argument
    End Sub

    Private Async Sub RequestToRefresh(sender As Object, e As MatEventArgs) Handles _
        Xl_Years1.AfterUpdate,
         Xl_BookFras1.RequestToRefresh

        Await refresca()
    End Sub

    Private Sub ExcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExcelToolStripMenuItem.Click
        Dim oSheet = FEB.Bookfras.Excel(_values, "factures rebudes", "M+O invoices")
        Dim exs As New List(Of Exception)
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub ZipAmbTotesLesFacturesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ZipAmbTotesLesFacturesToolStripMenuItem.Click
        Dim exs As New List(Of Exception)
        Dim CancelRequest As Boolean
        Dim oDlg As New SaveFileDialog
        With oDlg
            .Title = "Seleccionar una carpeta per desar les factures"
            .Filter = "fitxers comprimits (*.zip)|*.zip|tots els fitxers|*.*"
            .FileName = String.Format("factures rebudes any {0}.zip", Xl_Years1.Value)
            If .ShowDialog = DialogResult.OK Then
                Using zipToOpen As FileStream = New FileStream(.FileName, FileMode.Create)
                    Using archive As ZipArchive = New ZipArchive(zipToOpen, ZipArchiveMode.Update)

                        For Each item In _values
                            Dim label As String = String.Format("{0:dd.MM.yyyy} desant factura {1} de {2}", item.Cca.Fch, item.FraNum, item.Contact.Nom)
                            Xl_ProgressBar1.ShowProgress(1, _values.Count, _values.IndexOf(item) + 1, label, CancelRequest)
                            If CancelRequest Then Exit For

                            Dim oByteArray = Await FEB.BookFra.Pdf(exs, item)
                            If exs.Count = 0 Then
                                'Builds a filesystem friendly contact name
                                Dim friendlyContactName As String = "", friendlyFraNum As String = ""
                                Dim invalidFileNameChars = Path.GetInvalidFileNameChars()
                                If item.Contact IsNot Nothing AndAlso Not String.IsNullOrEmpty(item.Contact.Nom) Then
                                    friendlyContactName = New String(item.Contact.Nom.[Select](Function(ch) If(invalidFileNameChars.Contains(ch), "_"c, ch)).ToArray())
                                End If
                                friendlyFraNum = New String(item.FraNum.[Select](Function(ch) If(invalidFileNameChars.Contains(ch), "_"c, ch)).ToArray())
                                Dim filename = String.Format("registre {0:000000} factura {1} de {2}.pdf", item.Cca.Id, friendlyFraNum, friendlyContactName)

                                Dim entry As ZipArchiveEntry = archive.CreateEntry(filename)
                                Dim ms As New System.IO.MemoryStream(oByteArray)
                                Using entryStream = entry.Open()
                                    ms.CopyTo(entryStream)
                                End Using
                            Else
                                UIHelper.WarnError(exs)
                                Exit For
                            End If

                        Next
                    End Using
                End Using


                Xl_ProgressBar1.Visible = False
                If exs.Count = 0 Then
                    MsgBox("factures desades a " & oDlg.FileName, MsgBoxStyle.Information)
                Else
                    UIHelper.WarnError(exs)
                End If
            End If
        End With

    End Sub
End Class