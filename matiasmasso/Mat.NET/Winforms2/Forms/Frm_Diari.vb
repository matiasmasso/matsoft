Public Class Frm_Diari
    Private _Diari As DTODiari
    Private _AllowEvents As Boolean

    Private Async Sub Frm_Diari_Load(sender As Object, e As EventArgs) Handles Me.Load
        LoadYears()
        ComboBoxMode.SelectedIndex = 0
        Await refresca()
        _AllowEvents = True
    End Sub

    Private Async Function refresca() As Task
        _Diari = DTODiari.Factory(CurrentMode,
                                   Current.Session.User,
                                   oChannel:=CurrentChannel,
                                   oRep:=CurrentRep)

        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        _Diari = Await FEB.Diari.Load(_Diari, exs)
        ProgressBar1.Visible = False

        If exs.Count = 0 Then
            Xl_Diari_Months.Load(_Diari, 1)
            Xl_Diari_Days.Load(_Diari, 2)
            Xl_Diari_Pdcs.Load(_Diari, 3)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Function CurrentMode() As SelModes
        Dim retval As DTODiari.Modes = ComboBoxMode.SelectedIndex
        Return retval
    End Function

    Private Function CurrentChannel() As DTODistributionChannel
        Dim retval As DTODistributionChannel = Nothing
        If CheckBoxChannelFilter.Checked Then
            retval = Xl_LookupDistributionChannel1.DistributionChannel
        End If
        Return retval
    End Function

    Private Function CurrentRep() As DTORep
        Dim retval As DTORep = Nothing
        If CheckBoxRepFilter.Checked Then
            retval = Xl_LookupRep1.Rep
        End If
        Return retval
    End Function

    Private Sub LoadYears()
        Dim iYears As New List(Of Integer)
        For i As Integer = DTO.GlobalVariables.Today().Year To 1985 Step -1
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
            If oItem IsNot Nothing Then
                Xl_Diari_Pdcs.Load(_Diari, oItem.Index)
            End If
        End If
    End Sub

    Private Async Sub Xl_Years1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_Years1.AfterUpdate
        _AllowEvents = False
        _Diari = DTODiari.Factory(DTODiari.Modes.Pdcs, Current.Session.User, Xl_Years1.Value)

        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        _Diari = Await FEB.Diari.Load(_Diari, exs)
        ProgressBar1.Visible = False

        If exs.Count = 0 Then
            Xl_Diari_Months.Load(_Diari, 1)
            Xl_Diari_Days.Load(_Diari, 2)
            Xl_Diari_Pdcs.Load(_Diari, 3)
        Else
            UIHelper.WarnError(exs)
        End If

        _AllowEvents = True
    End Sub

    Private Async Sub ComboBoxMode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxMode.SelectedIndexChanged
        If _AllowEvents Then
            Await refresca()
        End If
    End Sub

    Private Async Sub CheckBoxChannelFilter_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxChannelFilter.CheckedChanged
        If _AllowEvents Then
            Xl_LookupDistributionChannel1.Visible = CheckBoxChannelFilter.Checked
            If Not CheckBoxChannelFilter.Checked Then Await refresca()
        End If
    End Sub

    Private Async Sub Xl_LookupDistributionChannel1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_LookupDistributionChannel1.AfterUpdate
        Await refresca()
    End Sub

    Private Async Sub CheckBoxRepFilter_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxRepFilter.CheckedChanged
        If _AllowEvents Then
            Xl_LookupRep1.Visible = CheckBoxRepFilter.Checked
            If Not CheckBoxRepFilter.Checked Then Await refresca()
        End If
    End Sub

    Private Async Sub Xl_LookupRep1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_LookupRep1.AfterUpdate
        Await refresca()
    End Sub
End Class