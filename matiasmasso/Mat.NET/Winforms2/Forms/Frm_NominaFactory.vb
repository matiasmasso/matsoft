Public Class Frm_NominaFactory
    Private _Nomina As DTONomina
    Private _AllowEvents As Boolean
    Private _Staffs As List(Of DTOStaff)
    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New()
        MyBase.New
        InitializeComponent()
        _Nomina = New DTONomina()
    End Sub

    Public Sub New(oStaff As DTOStaff)
        MyBase.New()
        Me.InitializeComponent()
        _Nomina = New DTONomina(oStaff)
    End Sub

    Public Sub New(oNomina As DTONomina)
        MyBase.New()
        Me.InitializeComponent()
        _Nomina = oNomina
    End Sub

    Private Async Sub Frm_Nomina_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        Dim oStaffs = Await FEB.Staffs.AllActive(Current.Session.Emp, exs)
        If exs.Count = 0 Then
            ComboBoxStaffs.DisplayMember = "Abr"
            ComboBoxStaffs.DataSource = oStaffs

            If _Nomina.IsNew Then
                If SelectedStaff() IsNot Nothing Then
                    Await Xl_Iban1.Load(SelectedStaff.Iban)
                End If
            Else
                If FEB.Nomina.Load(_Nomina, exs) Then
                    With _Nomina
                        ComboBoxStaffs.SelectedItem = oStaffs.FirstOrDefault(Function(x) x.Guid.Equals(.Staff.Guid))
                        Xl_AmountDevengat.Amt = .Devengat
                        Xl_AmountDietes.Amt = .Dietes
                        Xl_AmountSegSocial.Amt = .SegSocial
                        Xl_AmountBaseIrpf.Amt = .IrpfBase
                        Xl_AmountIrpf.Amt = .Irpf
                        Xl_AmountEmbargaments.Amt = .Embargos
                        Xl_AmountDeutes.Amt = .Deutes
                        Xl_AmountAnticips.Amt = .Anticips
                        Xl_AmountLiquid.Amt = .Liquid
                        Await Xl_Iban1.Load(.Iban)
                        ButtonOk.Enabled = .IsNew
                        ButtonDel.Enabled = Not .IsNew
                    End With
                Else
                    UIHelper.WarnError(exs)
                    Me.Close()
                End If
            End If
        Else
            UIHelper.WarnError(exs)
        End If
        _AllowEvents = True
    End Sub

    Private Function SelectedStaff() As DTOStaff
        Return ComboBoxStaffs.SelectedItem
    End Function

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        Xl_AmountDevengat.AfterUpdate,
         Xl_AmountDietes.AfterUpdate,
          Xl_AmountSegSocial.AfterUpdate,
           Xl_AmountBaseIrpf.AfterUpdate,
            Xl_AmountIrpf.AfterUpdate,
             Xl_AmountEmbargaments.AfterUpdate,
              Xl_AmountDeutes.AfterUpdate,
               Xl_AmountAnticips.AfterUpdate,
                Xl_AmountLiquid.AfterUpdate,
                 ComboBoxStaffs.SelectedIndexChanged,
                  Xl_DocFile1.AfterUpdate


        If _AllowEvents Then
            Dim liquid = Xl_AmountDevengat.Amt
            liquid.Add(Xl_AmountDietes.Amt)
            liquid.Substract(Xl_AmountSegSocial.Amt)
            liquid.Substract(Xl_AmountIrpf.Amt)
            liquid.Substract(Xl_AmountEmbargaments.Amt)
            liquid.Substract(Xl_AmountDeutes.Amt)
            liquid.Substract(Xl_AmountAnticips.Amt)
            Xl_AmountLiquid.Amt = liquid
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        Dim exs As New List(Of Exception)
        Dim fch = DateTimePicker1.Value
        Dim oCtas = Await FEB.PgcCtas.All(exs, fch.Year)
        If exs.Count = 0 Then
            With _Nomina
                .Staff = ComboBoxStaffs.SelectedItem
                .Devengat = Xl_AmountDevengat.Amt
                .Dietes = Xl_AmountDietes.Amt
                .SegSocial = Xl_AmountSegSocial.Amt
                .IrpfBase = Xl_AmountBaseIrpf.Amt
                .Irpf = Xl_AmountIrpf.Amt
                .Embargos = Xl_AmountEmbargaments.Amt
                .Deutes = Xl_AmountDeutes.Amt
                .Anticips = Xl_AmountAnticips.Amt
                .Liquid = Xl_AmountLiquid.Amt
                .Iban = Xl_Iban1.Value

                .Cca = DTOCca.Factory(DateTimePicker1.Value, Current.Session.User, DTOCca.CcdEnum.Nomina)
                .Cca.Concept = "Nómina " & .Staff.Abr
                .Cca.DocFile = Xl_DocFile1.Value
                .Cca.AddDebit(.Devengat, oCtas.FirstOrDefault(Function(x) x.Codi = DTOPgcPlan.Ctas.Nomina), .Staff)
                If .Dietes IsNot Nothing AndAlso .Dietes.IsNotZero Then .Cca.AddDebit(.Dietes, oCtas.FirstOrDefault(Function(x) x.Codi = DTOPgcPlan.Ctas.Nomina), .Staff)
                If .SegSocial IsNot Nothing AndAlso .SegSocial.IsNotZero Then .Cca.AddCredit(.SegSocial, oCtas.FirstOrDefault(Function(x) x.Codi = DTOPgcPlan.Ctas.SegSocialDevengo), .Staff)
                If .Irpf IsNot Nothing AndAlso .Irpf.IsNotZero Then .Cca.AddCredit(.Irpf, oCtas.FirstOrDefault(Function(x) x.Codi = DTOPgcPlan.Ctas.IrpfTreballadors), .Staff)
                If .Embargos IsNot Nothing AndAlso .Embargos.IsNotZero Then .Cca.AddCredit(.Embargos, oCtas.FirstOrDefault(Function(x) x.Codi = DTOPgcPlan.Ctas.NominaEmbargos), .Staff)
                If .Deutes IsNot Nothing AndAlso .Deutes.IsNotZero Then .Cca.AddCredit(.Deutes, oCtas.FirstOrDefault(Function(x) x.Codi = DTOPgcPlan.Ctas.NominaDeutes), .Staff)
                If .Anticips IsNot Nothing AndAlso .Anticips.IsNotZero Then .Cca.AddCredit(.Anticips, oCtas.FirstOrDefault(Function(x) x.Codi = DTOPgcPlan.Ctas.AnticipsTreballadors), .Staff)
                .Cca.AddSaldo(oCtas.FirstOrDefault(Function(x) x.Codi = DTOPgcPlan.Ctas.PagasTreballadors), .Staff)
            End With

            UIHelper.ToggleProggressBar(PanelButtons, True)
            If Await FEB.Nomina.Update(_Nomina, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Nomina))
                Me.Close()
            Else
                UIHelper.ToggleProggressBar(PanelButtons, False)
                UIHelper.WarnError(exs, "error al desar")
            End If
        Else
            UIHelper.WarnError(exs)
        End If

    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        Dim rc As MsgBoxResult = MsgBox("ho eliminem?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            UIHelper.ToggleProggressBar(PanelButtons, True)
            If Await FEB.Nomina.Delete(_Nomina, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Nomina))
                Me.Close()
            Else
                UIHelper.ToggleProggressBar(PanelButtons, False)
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub


End Class