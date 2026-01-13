Public Class Frm_Spvs

    Private _Customer As DTOCustomer
    Private _Spvs As List(Of DTOSpv)
    Private _IsDirty As Boolean
    Private _Allowevents As Boolean

    Public Sub New(Optional oCustomer As DTOCustomer = Nothing)
        MyBase.New()
        _Customer = oCustomer
        Me.InitializeComponent()
    End Sub

    Private Async Sub Frm_Spvs_Load(sender As Object, e As EventArgs) Handles Me.Load
        NumericUpDownYears.Value = DTO.GlobalVariables.Today().Year
        Await LoadSpvs()
        refresca()
    End Sub

    Private Async Sub Xl_Spvs1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Spvs1.RequestToRefresh
        Await LoadSpvs()
        refresca()
    End Sub

    Private Sub refresca()
        Dim exs As New List(Of Exception)
        Dim oSpvs = _Spvs
        If exs.Count = 0 Then
            If CheckBoxRead.Checked And CheckBoxNotRead.Checked Then
            ElseIf CheckBoxRead.Checked Then
                oSpvs = oSpvs.Where(Function(x) x.fchRead <> Nothing).ToList
            Else
                oSpvs = oSpvs.Where(Function(x) x.fchRead = Nothing).ToList
            End If

            If CheckBoxArrived.Checked And CheckBoxNotArrived.Checked Then
            ElseIf CheckBoxArrived.Checked Then
                oSpvs = oSpvs.Where(Function(x) x.spvIn IsNot Nothing Or x.usrOutOfSpvIn IsNot Nothing).ToList
            Else
                oSpvs = oSpvs.Where(Function(x) x.spvIn Is Nothing And x.usrOutOfSpvIn Is Nothing).ToList
            End If

            If CheckBoxShipped.Checked And CheckBoxNotShipped.Checked Then
            ElseIf CheckBoxShipped.Checked Then
                oSpvs = oSpvs.Where(Function(x) x.delivery IsNot Nothing Or x.usrOutOfSpvOut IsNot Nothing).ToList
            Else
                oSpvs = oSpvs.Where(Function(x) x.delivery Is Nothing And x.usrOutOfSpvOut Is Nothing).ToList
            End If

            Xl_Spvs1.Load(oSpvs, Extended:=True)
            LabelTot.Text = String.Format("total: {0:N0}", oSpvs.Count)
            _Allowevents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Function LoadSpvs() As Task
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        If (isOpen() Or _Spvs Is Nothing) Then
            _Spvs = Await FEB.Spvs.OpenHeaders(exs, Current.Session.Emp, _Customer)
        Else
            Dim year As Integer = 0
            If Not CheckBoxAllYears.Checked Then year = NumericUpDownYears.Value
            _Spvs = Await FEB.Spvs.Headers(exs, Current.Session.Emp, _Customer, year)
        End If
        ProgressBar1.Visible = False
    End Function

    Private Function isOpen() As Boolean
        Dim retval As Boolean = CheckBoxNotRead.Checked And CheckBoxRead.Checked And CheckBoxArrived.Checked And CheckBoxNotArrived.Checked And CheckBoxNotShipped.Checked And Not CheckBoxShipped.Checked
        Return retval
    End Function


    Private Sub CheckBoxRead_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxRead.CheckedChanged
        CheckedChangedHandler(CheckBoxRead, CheckBoxNotRead)
    End Sub

    Private Sub CheckBoxNotRead_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxNotRead.CheckedChanged
        CheckedChangedHandler(CheckBoxNotRead, CheckBoxRead)
    End Sub

    Private Sub CheckBoxArrived_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxArrived.CheckedChanged
        CheckedChangedHandler(CheckBoxArrived, CheckBoxNotArrived)
    End Sub

    Private Sub CheckBoxNotArrived_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxNotArrived.CheckedChanged
        CheckedChangedHandler(CheckBoxNotArrived, CheckBoxArrived)
    End Sub

    Private Sub CheckBoxShipped_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxShipped.CheckedChanged
        CheckedChangedHandler(CheckBoxShipped, CheckBoxNotShipped)
    End Sub

    Private Sub CheckBoxNotShipped_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxNotShipped.CheckedChanged
        CheckedChangedHandler(CheckBoxNotShipped, CheckBoxShipped)
    End Sub

    Private Async Sub CheckedChangedHandler(sender As CheckBox, alternative As CheckBox)
        If _Allowevents Then
            If Not sender.Checked Then
                _Allowevents = False
                alternative.Checked = True
                _Allowevents = True
            End If
            If isOpen() Then
                NumericUpDownYears.Visible = False
                CheckBoxAllYears.Checked = True
                CheckBoxAllYears.Enabled = False
            Else
                NumericUpDownYears.Visible = True
                If _Spvs.All(Function(x) x.delivery Is Nothing) Then
                    CheckBoxAllYears.Visible = True
                    CheckBoxAllYears.Enabled = True
                    NumericUpDownYears.Visible = Not CheckBoxAllYears.Checked
                    Await LoadSpvs()
                End If
            End If
            refresca()
        End If
    End Sub

    Private Sub Xl_TextboxSearch1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_TextboxSearch1.AfterUpdate
        Xl_Spvs1.Filter = e.Argument
    End Sub
End Class