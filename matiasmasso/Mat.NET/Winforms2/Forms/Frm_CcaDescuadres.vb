Public Class Frm_CcaDescuadres
    Private _Exercici As DTOExercici
    Private _AllowEvents As Boolean

    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oExercici As DTOExercici = Nothing)
        InitializeComponent()
        _Exercici = oExercici
    End Sub

    Private Async Sub Frm_CcaDescuadres_Load(sender As Object, e As EventArgs) Handles Me.Load
        If _Exercici Is Nothing Then _Exercici = DTOExercici.Current(Current.Session.Emp)
        Dim years = Enumerable.Range(start:=1985, count:=_Exercici.Year + 1 - 1985).Reverse().ToList()
        Xl_Years1.Load(years, _Exercici.Year)
        _AllowEvents = True
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        Dim oCcas = Await FEB.Ccas.Descuadres(exs, _Exercici)
        ProgressBar1.Visible = False
        If exs.Count = 0 Then
            If oCcas.Count = 0 Then
                MsgBox("No s'ha trobat cap assentament desquadrat a " & _Exercici.Year.ToString(), MsgBoxStyle.Information)
            Else
                Xl_Ccas1.Load(oCcas)
            End If
        Else
            UIHelper.WarnError(exs)
        End If

    End Function

    Private Sub Xl_Ccas1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Ccas1.RequestToRefresh
        RaiseEvent RequestToRefresh(Me, e)
    End Sub

    Private Async Sub Xl_Years1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_Years1.AfterUpdate
        If _allowevents Then
            _Exercici = DTOExercici.FromYear(Current.Session.Emp, Xl_Years1.Value)
            Await refresca()
        End If
    End Sub
End Class