Public Class Frm_Statement
    Private _Year As Integer
    Private _Contact As DTOContact
    Private _Cta As DTOPgcCta
    Private _Statement As DTOStatement
    Private _allowevents As Boolean

    Public Sub New(Optional oContact As DTOContact = Nothing, Optional oCta As DTOPgcCta = Nothing, Optional year As Integer = 0)
        MyBase.New
        InitializeComponent()
        _Year = year
        _Contact = oContact
        _Cta = oCta
    End Sub

    Private Async Sub Frm_Statement_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        Dim years = Await FEB.Statement.Years(exs, _Contact)
        If exs.Count = 0 Then
            If years.Count > 0 Then
                If _Year = 0 Then _Year = years.First()
                Xl_StatementYears1.Load(years, _Year)
                Await LoadCtas()
                ProgressBar1.Visible = False
                _allowevents = True
            Else
                ProgressBar1.Visible = False
                MsgBox("No hi han registres comptables en aquesta fitxa", MsgBoxStyle.Exclamation)
            End If
        Else
            ProgressBar1.Visible = False
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Xl_StatementYears1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_StatementYears1.ValueChanged
        If _allowevents Then
            Await LoadCtas()
        End If
    End Sub

    Private Sub Xl_StatementCtas1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_StatementCtas1.ValueChanged
        If _allowevents Then
            Dim oitems = _Statement.Items.Where(Function(x) x.CtaGuid.Equals(CurrentCta.Guid)).ToList()
            Xl_StatementItems1.Load(oitems, oitems.Last)
        End If
    End Sub

    Private Function CurrentYear() As Integer
        Return Xl_StatementYears1.Value
    End Function

    Private Function CurrentCta() As DTOPgcCta
        Return Xl_StatementCtas1.Value
    End Function

    Private Async Function LoadCtas() As Task
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        _Statement = Await FEB.Statement.Fetch(exs, _Contact, CurrentYear())
        ProgressBar1.Visible = False
        If exs.Count = 0 Then
            Xl_StatementCtas1.Load(_Statement, _Cta)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

End Class