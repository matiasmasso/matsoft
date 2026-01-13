Public Class Frm_Diari
    Private _Diari As DTODiari
    Private _AllowEvents As Boolean

    Private Sub Frm_Diari_Load(sender As Object, e As EventArgs) Handles Me.Load
        LoadYears()
        _Diari = BLL.BLLDiari.GetDiari(BLL.BLLSession.Current.User)
        Xl_Diari_Months.Load(_Diari, 1)
        Xl_Diari_Days.Load(_Diari, 2)
        Xl_Diari_Pdcs.Load(_Diari, 3)
        _AllowEvents = True
    End Sub

    Private Sub LoadYears()
        Dim iYears As New List(Of Integer)
        For i As Integer = Today.Year To 1985 Step -1
            iYears.Add(i)
        Next
        Xl_Years1.Load(iYears)
    End Sub

    Private Sub Xl_Diari_Months_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_Diari_Months.ValueChanged
        If _AllowEvents Then
            _AllowEvents = False
            Dim oItem As DtoDiariItem = e.Argument
            Xl_Diari_Days.Load(_Diari, oItem.Index)
            Xl_Diari_Pdcs.Load(_Diari, oItem.Index + 1)
            _AllowEvents = True
        End If
    End Sub

    Private Sub Xl_Diari_Days_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_Diari_Days.ValueChanged
        If _AllowEvents Then
            Dim oItem As DtoDiariItem = e.Argument
            Xl_Diari_Pdcs.Load(_Diari, oItem.Index)
        End If
    End Sub

    Private Sub Xl_Years1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_Years1.AfterUpdate
        _AllowEvents = False
        _Diari = BLL.BLLDiari.GetDiari(BLL.BLLSession.Current.User, Xl_Years1.Value)
        Xl_Diari_Months.Load(_Diari, 1)
        Xl_Diari_Days.Load(_Diari, 2)
        Xl_Diari_Pdcs.Load(_Diari, 3)
        _AllowEvents = True
    End Sub
End Class