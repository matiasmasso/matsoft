Public Class Frm_TrpZon

    Private _TrpZon As DTOTrpZon

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(oTrpZon As DTOTrpZon)
        MyBase.New
        Me.InitializeComponent()
        _TrpZon = oTrpZon
    End Sub

    Private Sub Frm_TrpZon_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB2.TrpZon.Load(_TrpZon, exs) Then
            TextBoxTrpZonaNom.ReadOnly = Not _TrpZon.IsNew
            TextBoxTransportista.Text = _TrpZon.Transportista.Abr
            TextBoxTrpZonaNom.Text = _TrpZon.Nom
            CheckBoxActivat.Checked = _TrpZon.Activat
            If _TrpZon.Cubicatje = 0 Then
                If _TrpZon.Transportista.Cubicaje <> 0 Then
                    NumericUpDownCubicatje.Value = _TrpZon.Transportista.Cubicaje
                    NumericUpDownCubicatje.ReadOnly = True
                    CheckBoxInheritCubicatje.Checked = True
                End If
            Else
                NumericUpDownCubicatje.Value = _TrpZon.Cubicatje
                NumericUpDownCubicatje.ReadOnly = False
                CheckBoxInheritCubicatje.Checked = False
            End If

            CheckBoxActivat.Checked = _TrpZon.Activat

            Xl_TrpCosts1.Load(_TrpZon.Costs)
            Xl_ZonasIsoPais1.Load(_TrpZon.Zonas)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Xl_ZonasIsoPais1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_ZonasIsoPais1.RequestToAddNew
        Dim oFrm As New Frm_Geo(DTOArea.SelectModes.SelectZona)
        AddHandler oFrm.onItemSelected, AddressOf onZonaAdded
        oFrm.Show()
    End Sub

    Private Async Sub onZonaAdded(sender As Object, e As MatEventArgs)
        GetFromForm()
        _TrpZon.Zonas.Add(e.Argument)

        Dim exs As New List(Of Exception)
        If Await FEB2.TrpZon.Update(_TrpZon, exs) Then
            Xl_ZonasIsoPais1.Load(_TrpZon.Zonas)
            ButtonDel.Enabled = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Xl_ZonasIsoPais1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_ZonasIsoPais1.RequestToRefresh
        Xl_ZonasIsoPais1.Load(_TrpZon.Zonas)
    End Sub

    Private Async Sub Xl_ZonasIsoPais1_RequestToDelete(sender As Object, e As MatEventArgs) Handles Xl_ZonasIsoPais1.RequestToDelete
        Dim oZonas As List(Of DTOZona) = Xl_ZonasIsoPais1.Values
        oZonas.Remove(e.Argument)
        _TrpZon.Zonas = oZonas
        Dim exs As New List(Of Exception)
        If Await FEB2.TrpZon.Update(_TrpZon, exs) Then
            Xl_ZonasIsoPais1.Load(_TrpZon.Zonas)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim rc As MsgBoxResult = MsgBox(String.Format("Eliminem {0}?", _TrpZon.Nom), MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB2.TrpZon.Delete(_TrpZon, exs) Then
                RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar el document")
            End If
        Else
            MsgBox("Operació cancel·lada per l'usuari", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub GetFromForm()
        With _TrpZon
            .Nom = TextBoxTrpZonaNom.Text
            If CheckBoxInheritCubicatje.Checked Then
                .Cubicatje = 0
            Else
                .Cubicatje = NumericUpDownCubicatje.Value
            End If
            .Activat = CheckBoxActivat.Checked
            .Zonas = Xl_ZonasIsoPais1.Values
            .Costs = Xl_TrpCosts1.Values
        End With
    End Sub
    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        GetFromForm()
        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        If Await FEB2.TrpZon.Update(_TrpZon, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_TrpZon))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub CheckBoxInheritCubicatje_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxInheritCubicatje.CheckedChanged
        If CheckBoxInheritCubicatje.Checked Then
            If _TrpZon.Transportista.Cubicaje <> 0 Then
                NumericUpDownCubicatje.Value = _TrpZon.Transportista.Cubicaje
                NumericUpDownCubicatje.ReadOnly = True
                NumericUpDownCubicatje.Enabled = False
            End If
        Else
            NumericUpDownCubicatje.Value = _TrpZon.Cubicatje
            NumericUpDownCubicatje.ReadOnly = False
            NumericUpDownCubicatje.Enabled = True
        End If
    End Sub
End Class