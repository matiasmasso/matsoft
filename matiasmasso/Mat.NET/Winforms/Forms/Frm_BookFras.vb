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
            For i As Integer = Today.Year To 1985 Step -1
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
        _values = Await FEB2.Bookfras.All(exs, DTOBookFra.Modes.All, oExercici)
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
        Dim oSheet = FEB2.Bookfras.Excel(_values, "factures rebudes", "M+O invoices")
        Dim exs As New List(Of Exception)
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub
End Class