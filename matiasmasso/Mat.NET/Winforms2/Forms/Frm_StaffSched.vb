Public Class Frm_StaffSched
    Private _value As DTOStaffSched
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOStaffSched)
        MyBase.New
        InitializeComponent()

        _value = value
    End Sub

    Private Sub Frm_StaffSched_Load(sender As Object, e As EventArgs) Handles Me.Load
        setTitle()
        DateTimePickerFchFrom.Value = _value.FchFrom
        If _value.FchTo <> Nothing Then
            CheckBoxObsolet.Checked = True
            DateTimePickerFchTo.Visible = True
            DateTimePickerFchTo.Value = _value.FchTo
        End If
        Xl_StaffSched1.Load(_value, DTOStaffSched.Item.Cods.Ordinari)
        Xl_StaffSched2.Load(_value, DTOStaffSched.Item.Cods.Intensiu)
        _AllowEvents = True
    End Sub

    Private Sub setTitle()
        If _value.Staff Is Nothing Then
            Me.Text = "Horaris jornada per defecte (" & _value.Emp.Nom & ")"
        Else
            Me.Text = "Horaris jornada " & _value.Staff.Abr
        End If
    End Sub

    Private Sub Xl_StaffSched1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_StaffSched1.AfterUpdate
        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        Dim exs As New List(Of Exception)
        With _value
            .FchFrom = DateTimePickerFchFrom.Value
            If CheckBoxObsolet.Checked Then
                .FchTo = DateTimePickerFchTo.Value
            Else
                .FchTo = Nothing
            End If
            .Items = Xl_StaffSched1.Values
            .Items.AddRange(Xl_StaffSched2.Values)
        End With
        UIHelper.ToggleProggressBar(Panel1, True)
        If Await FEB.StaffSched.Update(_value, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_value))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        If Await FEB.StaffSched.Delete(_value, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_value))
            Me.Close()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub CheckBoxObsolet_Click(sender As Object, e As EventArgs) Handles CheckBoxObsolet.Click
        If _AllowEvents Then
            DateTimePickerFchTo.Visible = CheckBoxObsolet.Checked
        End If
    End Sub
End Class