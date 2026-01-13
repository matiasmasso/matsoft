

Public Class Frm_ECI
    Private mEmp as DTOEmp = BLL.BLLApp.Emp
    Private mAllowEvents As Boolean

    Private Sub Frm_ECI_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Xl_Contact1.Contact = MaxiSrvr.Contact.FromNum(mEmp, ECI.Id)
        TextBoxDepto.Text = ECI.Departamento
        CheckBoxAgrupar.Checked = ECI.Agrupar
        GroupBoxAgrupar.Enabled = CheckBoxAgrupar.Checked
        Weekday = ECI.WeekDay
        mAllowEvents = True
    End Sub

    Private Property Weekday()
        Get
            Dim RetVal As Integer = -1
            If RadioButtonWeek1.Checked Then RetVal = 1
            If RadioButtonWeek2.Checked Then RetVal = 2
            If RadioButtonWeek3.Checked Then RetVal = 3
            If RadioButtonWeek4.Checked Then RetVal = 4
            If RadioButtonWeek5.Checked Then RetVal = 5
            Return RetVal
        End Get
        Set(ByVal value)
            Select Case value
                Case 1
                    RadioButtonWeek1.Checked = True
                Case 2
                    RadioButtonWeek2.Checked = True
                Case 3
                    RadioButtonWeek3.Checked = True
                Case 4
                    RadioButtonWeek4.Checked = True
                Case 5
                    RadioButtonWeek5.Checked = True
            End Select
        End Set
    End Property

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        ECI.Id = Xl_Contact1.Contact.Id
        ECI.Departamento = TextBoxDepto.Text
        ECI.Agrupar = CheckBoxAgrupar.Checked
        ECI.WeekDay = Weekday
        Me.Close()
    End Sub

    Private Sub DirtyControls(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    Xl_Contact1.AfterUpdate, _
     TextBoxDepto.TextChanged, _
       RadioButtonWeek1.CheckedChanged, _
       RadioButtonWeek2.CheckedChanged, _
       RadioButtonWeek3.CheckedChanged, _
       RadioButtonWeek4.CheckedChanged, _
       RadioButtonWeek5.CheckedChanged
        If mAllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub CheckBoxAgrupar_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxAgrupar.CheckedChanged
        GroupBoxAgrupar.Enabled = CheckBoxAgrupar.Checked
        If mAllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub
End Class