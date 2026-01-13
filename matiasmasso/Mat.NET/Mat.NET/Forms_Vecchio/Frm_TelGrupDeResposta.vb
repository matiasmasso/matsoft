

Public Class Frm_TelGrupDeResposta
    Private mGrupDeResposta As TelGrupDeResposta
    Private mAllowEvents As Boolean = False

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oAgrupacio As TelGrupDeResposta)
        MyBase.new()
        Me.InitializeComponent()
        mGrupDeResposta = oAgrupacio
        'Me.Text = mObject.ToString
        Refresca()
        mAllowEvents = True
    End Sub

    Private Sub Refresca()
        With mGrupDeResposta
            'TextBoxNom.Text = .text
            If .Exists Then
                TextBoxNom.Text = .Nom
                RadioButtonCuaDeTrucades.Checked = .CuaDeTrucades
                RadioButtonBusy.Checked = Not .CuaDeTrucades
                Xl_Lookup_TelTimbres1.Timbre = .Timbre
                ButtonDel.Enabled = .AllowDelete
                LoadLinies()
                LoadUsuaris()
                LoadHoraris()
                EnableDateTimePickers()
                CheckBoxContestadorActivat.Checked = .ContestadorActivat
            End If
        End With
    End Sub


    Private Sub LoadLinies()
        Dim oSrc As TelLinies = TelLinies.Actives
        For Each oLinia As TelLinia In oSrc
            If mGrupDeResposta.Equals(oLinia.GrupDeResposta) Then
                CheckedListBoxLinies.Items.Add(oLinia.Id, True)
            ElseIf oLinia.GrupDeResposta Is Nothing Then
                CheckedListBoxLinies.Items.Add(oLinia.Id, False)
            End If
        Next
    End Sub

    Private Sub LoadUsuaris()
        For Each oUsr As Usr In Usrs.EnabledForCommunications
            CheckedListBoxUsrs.Items.Add(oUsr.login, mGrupDeResposta.Contains(oUsr))
        Next
    End Sub

    Private Sub LoadHoraris()
        With mGrupDeResposta.Horari
            CheckBoxD1.Checked = .DayEnabled(DayOfWeek.Monday)
            Dim DtBase As Date = DateTimePicker.MinimumDateTime
            DateTimePickerD1MatinsStart.Value = DtBase.Add(.TimeMatinsStart(DayOfWeek.Monday).TimeOfDay)
            DateTimePickerD1MatinsEnd.Value = DtBase.Add(.TimeMatinsEnd(DayOfWeek.Monday).TimeOfDay)
            DateTimePickerD1TardesStart.Value = DtBase.Add(.TimeTardesStart(DayOfWeek.Monday).TimeOfDay)
            DateTimePickerD1TardesEnd.Value = DtBase.Add(.TimeTardesEnd(DayOfWeek.Monday).TimeOfDay)
            CheckBoxD2.Checked = .DayEnabled(DayOfWeek.Tuesday)
            DateTimePickerD2MatinsStart.Value = DtBase.Add(.TimeMatinsStart(DayOfWeek.Tuesday).TimeOfDay)
            DateTimePickerD2MatinsEnd.Value = DtBase.Add(.TimeMatinsEnd(DayOfWeek.Tuesday).TimeOfDay)
            DateTimePickerD2TardesStart.Value = DtBase.Add(.TimeTardesStart(DayOfWeek.Tuesday).TimeOfDay)
            DateTimePickerD2TardesEnd.Value = DtBase.Add(.TimeTardesEnd(DayOfWeek.Tuesday).TimeOfDay)
            CheckBoxD3.Checked = .DayEnabled(DayOfWeek.Wednesday)
            DateTimePickerD3MatinsStart.Value = DtBase.Add(.TimeMatinsStart(DayOfWeek.Wednesday).TimeOfDay)
            DateTimePickerD3MatinsEnd.Value = DtBase.Add(.TimeMatinsEnd(DayOfWeek.Wednesday).TimeOfDay)
            DateTimePickerD3TardesStart.Value = DtBase.Add(.TimeTardesStart(DayOfWeek.Wednesday).TimeOfDay)
            DateTimePickerD3TardesEnd.Value = DtBase.Add(.TimeTardesEnd(DayOfWeek.Wednesday).TimeOfDay)
            CheckBoxD4.Checked = .DayEnabled(DayOfWeek.Thursday)
            DateTimePickerD4MatinsStart.Value = DtBase.Add(.TimeMatinsStart(DayOfWeek.Thursday).TimeOfDay)
            DateTimePickerD4MatinsEnd.Value = DtBase.Add(.TimeMatinsEnd(DayOfWeek.Thursday).TimeOfDay)
            DateTimePickerD4TardesStart.Value = DtBase.Add(.TimeTardesStart(DayOfWeek.Thursday).TimeOfDay)
            DateTimePickerD4TardesEnd.Value = DtBase.Add(.TimeTardesEnd(DayOfWeek.Thursday).TimeOfDay)
            CheckBoxD5.Checked = .DayEnabled(DayOfWeek.Friday)
            DateTimePickerD5MatinsStart.Value = DtBase.Add(.TimeMatinsStart(DayOfWeek.Friday).TimeOfDay)
            DateTimePickerD5MatinsEnd.Value = DtBase.Add(.TimeMatinsEnd(DayOfWeek.Friday).TimeOfDay)
            DateTimePickerD5TardesStart.Value = DtBase.Add(.TimeTardesStart(DayOfWeek.Friday).TimeOfDay)
            DateTimePickerD5TardesEnd.Value = DtBase.Add(.TimeTardesEnd(DayOfWeek.Friday).TimeOfDay)
            CheckBoxD6.Checked = .DayEnabled(DayOfWeek.Saturday)
            DateTimePickerD6MatinsStart.Value = DtBase.Add(.TimeMatinsStart(DayOfWeek.Saturday).TimeOfDay)
            DateTimePickerD6MatinsEnd.Value = DtBase.Add(.TimeMatinsEnd(DayOfWeek.Saturday).TimeOfDay)
            DateTimePickerD6TardesStart.Value = DtBase.Add(.TimeTardesStart(DayOfWeek.Saturday).TimeOfDay)
            DateTimePickerD6TardesEnd.Value = DtBase.Add(.TimeTardesEnd(DayOfWeek.Saturday).TimeOfDay)
            CheckBoxD7.Checked = .DayEnabled(DayOfWeek.Sunday)
            DateTimePickerD7MatinsStart.Value = DtBase.Add(.TimeMatinsStart(DayOfWeek.Sunday).TimeOfDay)
            DateTimePickerD7MatinsEnd.Value = DtBase.Add(.TimeMatinsEnd(DayOfWeek.Sunday).TimeOfDay)
            DateTimePickerD7TardesStart.Value = DtBase.Add(.TimeTardesStart(DayOfWeek.Sunday).TimeOfDay)
            DateTimePickerD7TardesEnd.Value = DtBase.Add(.TimeTardesEnd(DayOfWeek.Sunday).TimeOfDay)
        End With

    End Sub

    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        TextBoxNom.TextChanged, _
         RadioButtonCuaDeTrucades.CheckedChanged, _
          RadioButtonBusy.CheckedChanged, _
         Xl_Lookup_TelTimbres1.AfterUpdate, _
         CheckedListBoxLinies.ItemCheck, _
          CheckedListBoxUsrs.ItemCheck, _
           CheckBoxContestadorActivat.CheckedChanged, _
           DateTimePickerD1MatinsStart.ValueChanged, _
           DateTimePickerD1MatinsEnd.ValueChanged, _
           DateTimePickerD1TardesStart.ValueChanged, _
           DateTimePickerD1TardesEnd.ValueChanged, _
           DateTimePickerD2MatinsStart.ValueChanged, _
           DateTimePickerD2MatinsEnd.ValueChanged, _
           DateTimePickerD2TardesStart.ValueChanged, _
           DateTimePickerD2TardesEnd.ValueChanged, _
           DateTimePickerD3MatinsStart.ValueChanged, _
           DateTimePickerD3MatinsEnd.ValueChanged, _
           DateTimePickerD3TardesStart.ValueChanged, _
           DateTimePickerD3TardesEnd.ValueChanged, _
           DateTimePickerD4MatinsStart.ValueChanged, _
           DateTimePickerD4MatinsEnd.ValueChanged, _
           DateTimePickerD4TardesStart.ValueChanged, _
           DateTimePickerD4TardesEnd.ValueChanged, _
           DateTimePickerD5MatinsStart.ValueChanged, _
           DateTimePickerD5MatinsEnd.ValueChanged, _
           DateTimePickerD5TardesStart.ValueChanged, _
           DateTimePickerD5TardesEnd.ValueChanged, _
           DateTimePickerD6MatinsStart.ValueChanged, _
           DateTimePickerD6MatinsEnd.ValueChanged, _
           DateTimePickerD6TardesStart.ValueChanged, _
           DateTimePickerD6TardesEnd.ValueChanged, _
           DateTimePickerD7MatinsStart.ValueChanged, _
           DateTimePickerD7MatinsEnd.ValueChanged, _
           DateTimePickerD7TardesStart.ValueChanged, _
           DateTimePickerD7TardesEnd.ValueChanged

        If mAllowEvents Then
            SetDirty()
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With mGrupDeResposta
            .Nom = TextBoxNom.Text
            .CuaDeTrucades = RadioButtonCuaDeTrucades.Checked
            .Timbre = Xl_Lookup_TelTimbres1.Timbre

            Dim oLinies As New TelLinies
            For Each sId As String In CheckedListBoxLinies.CheckedItems
                Dim oLinia As New TelLinia(sId)
                oLinies.Add(oLinia)
            Next
            .Linies = olinies

            Dim oUsrs As New Usrs
            For Each sLogin As String In CheckedListBoxUsrs.CheckedItems
                Dim oUsr As Usr = Usr.FromLogin(sLogin)
                oUsrs.Add(oUsr)
            Next
            .Usrs = oUsrs

            .ContestadorActivat = CheckBoxContestadorActivat.Checked

            With .Horari
                .DayEnabled(DayOfWeek.Monday) = CheckBoxD1.Checked
                .TimeMatinsStart(DayOfWeek.Monday) = DateTimePickerD1MatinsStart.Value
                .TimeMatinsEnd(DayOfWeek.Monday) = DateTimePickerD1MatinsEnd.Value
                .TimeTardesStart(DayOfWeek.Monday) = DateTimePickerD1TardesStart.Value
                .TimeTardesEnd(DayOfWeek.Monday) = DateTimePickerD1TardesEnd.Value
                .DayEnabled(DayOfWeek.Tuesday) = CheckBoxD2.Checked
                .TimeMatinsStart(DayOfWeek.Tuesday) = DateTimePickerD2MatinsStart.Value
                .TimeMatinsEnd(DayOfWeek.Tuesday) = DateTimePickerD2MatinsEnd.Value
                .TimeTardesStart(DayOfWeek.Tuesday) = DateTimePickerD2TardesStart.Value
                .TimeTardesEnd(DayOfWeek.Tuesday) = DateTimePickerD2TardesEnd.Value
                .DayEnabled(DayOfWeek.Wednesday) = CheckBoxD3.Checked
                .TimeMatinsStart(DayOfWeek.Wednesday) = DateTimePickerD3MatinsStart.Value
                .TimeMatinsEnd(DayOfWeek.Wednesday) = DateTimePickerD3MatinsEnd.Value
                .TimeTardesStart(DayOfWeek.Wednesday) = DateTimePickerD3TardesStart.Value
                .TimeTardesEnd(DayOfWeek.Wednesday) = DateTimePickerD3TardesEnd.Value
                .DayEnabled(DayOfWeek.Thursday) = CheckBoxD4.Checked
                .TimeMatinsStart(DayOfWeek.Thursday) = DateTimePickerD4MatinsStart.Value
                .TimeMatinsEnd(DayOfWeek.Thursday) = DateTimePickerD4MatinsEnd.Value
                .TimeTardesStart(DayOfWeek.Thursday) = DateTimePickerD4TardesStart.Value
                .TimeTardesEnd(DayOfWeek.Thursday) = DateTimePickerD4TardesEnd.Value
                .DayEnabled(DayOfWeek.Friday) = CheckBoxD5.Checked
                .TimeMatinsStart(DayOfWeek.Friday) = DateTimePickerD5MatinsStart.Value
                .TimeMatinsEnd(DayOfWeek.Friday) = DateTimePickerD5MatinsEnd.Value
                .TimeTardesStart(DayOfWeek.Friday) = DateTimePickerD5TardesStart.Value
                .TimeTardesEnd(DayOfWeek.Friday) = DateTimePickerD5TardesEnd.Value
                .DayEnabled(DayOfWeek.Saturday) = CheckBoxD6.Checked
                .TimeMatinsStart(DayOfWeek.Saturday) = DateTimePickerD6MatinsStart.Value
                .TimeMatinsEnd(DayOfWeek.Saturday) = DateTimePickerD6MatinsEnd.Value
                .TimeTardesStart(DayOfWeek.Saturday) = DateTimePickerD6TardesStart.Value
                .TimeTardesEnd(DayOfWeek.Saturday) = DateTimePickerD6TardesEnd.Value
                .DayEnabled(DayOfWeek.Sunday) = CheckBoxD7.Checked
                .TimeMatinsStart(DayOfWeek.Sunday) = DateTimePickerD7MatinsStart.Value
                .TimeMatinsEnd(DayOfWeek.Sunday) = DateTimePickerD7MatinsEnd.Value
                .TimeTardesStart(DayOfWeek.Sunday) = DateTimePickerD7TardesStart.Value
                .TimeTardesEnd(DayOfWeek.Sunday) = DateTimePickerD7TardesEnd.Value
            End With
            .Update()
            RaiseEvent AfterUpdate(mGrupDeResposta, System.EventArgs.Empty)
            Me.Close()
        End With
    End Sub

    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        If mGrupDeResposta.AllowDelete Then
            mGrupDeResposta.Delete()
        End If
    End Sub

    Private Sub SetDirty()
        ButtonOk.Enabled = True
    End Sub

    Private Sub CheckBoxD_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        CheckBoxD1.CheckedChanged, _
        CheckBoxD2.CheckedChanged, _
        CheckBoxD3.CheckedChanged, _
        CheckBoxD4.CheckedChanged, _
        CheckBoxD5.CheckedChanged, _
        CheckBoxD6.CheckedChanged, _
        CheckBoxD7.CheckedChanged

        EnableDateTimePickers()
        SetDirty()
    End Sub

 
    Private Sub EnableDateTimePickers()
        DateTimePickerD1MatinsStart.Enabled = CheckBoxD1.Checked
        DateTimePickerD1MatinsEnd.Enabled = CheckBoxD1.Checked
        DateTimePickerD1TardesStart.Enabled = CheckBoxD1.Checked
        DateTimePickerD1TardesEnd.Enabled = CheckBoxD1.Checked

        DateTimePickerD2MatinsStart.Enabled = CheckBoxD2.Checked
        DateTimePickerD2MatinsEnd.Enabled = CheckBoxD2.Checked
        DateTimePickerD2TardesStart.Enabled = CheckBoxD2.Checked
        DateTimePickerD2TardesEnd.Enabled = CheckBoxD2.Checked

        DateTimePickerD3MatinsStart.Enabled = CheckBoxD3.Checked
        DateTimePickerD3MatinsEnd.Enabled = CheckBoxD3.Checked
        DateTimePickerD3TardesStart.Enabled = CheckBoxD3.Checked
        DateTimePickerD3TardesEnd.Enabled = CheckBoxD3.Checked

        DateTimePickerD4MatinsStart.Enabled = CheckBoxD4.Checked
        DateTimePickerD4MatinsEnd.Enabled = CheckBoxD4.Checked
        DateTimePickerD4TardesStart.Enabled = CheckBoxD4.Checked
        DateTimePickerD4TardesEnd.Enabled = CheckBoxD4.Checked

        DateTimePickerD5MatinsStart.Enabled = CheckBoxD5.Checked
        DateTimePickerD5MatinsEnd.Enabled = CheckBoxD5.Checked
        DateTimePickerD5TardesStart.Enabled = CheckBoxD5.Checked
        DateTimePickerD5TardesEnd.Enabled = CheckBoxD5.Checked

        DateTimePickerD6MatinsStart.Enabled = CheckBoxD6.Checked
        DateTimePickerD6MatinsEnd.Enabled = CheckBoxD6.Checked
        DateTimePickerD6TardesStart.Enabled = CheckBoxD6.Checked
        DateTimePickerD6TardesEnd.Enabled = CheckBoxD6.Checked

        DateTimePickerD7MatinsStart.Enabled = CheckBoxD7.Checked
        DateTimePickerD7MatinsEnd.Enabled = CheckBoxD7.Checked
        DateTimePickerD7TardesStart.Enabled = CheckBoxD7.Checked
        DateTimePickerD7TardesEnd.Enabled = CheckBoxD7.Checked
    End Sub
End Class