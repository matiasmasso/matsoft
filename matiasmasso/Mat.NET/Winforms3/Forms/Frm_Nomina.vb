Public Class Frm_Nomina
    Private _Nomina As DTONomina

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(oNomina As DTONomina)
        MyBase.New()
        Me.InitializeComponent()
        _Nomina = oNomina
    End Sub

    Private Async Sub Frm_Nomina_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If Not _Nomina.IsNew Then
            If FEB.Nomina.Load(_Nomina, exs) Then
            Else
                UIHelper.WarnError(exs)
                Exit Sub
            End If
        End If

        With _Nomina
                TextBoxStaffNom.Text = .Staff.Nom
                DateTimePicker1.Value = .Cca.Fch
                Xl_NominaItems1.Load(.Items)
                Xl_AmtDevengat.Amt = .Devengat
                Xl_AmtDietes.Amt = .Dietes
                Xl_AmtSegSocial.Amt = .SegSocial
                Xl_AmtBaseIRPF.Amt = .IrpfBase
                Xl_AmtIRPF.Amt = .Irpf
                Xl_AmtEmbargos.Amt = .Embargos
                Xl_AmtDeutes.Amt = .Deutes
                Xl_AmtAnticips.Amt = .Anticips
            Await Xl_Iban1.Load(.Iban, .Staff, DTOIban.Cods.staff)
        End With
    End Sub

    Private Async Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        If Await FEB.Nomina.Delete(_Nomina, exs) Then
            RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
            Me.Close()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub


End Class