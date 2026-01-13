Public Class Frm_Nomina

    Public Sub New(oNomina As Nomina)
        MyBase.New()
        Me.InitializeComponent()
        With oNomina
            TextBoxStaffNom.Text = .Staff.Nom
            DateTimePicker1.Value = .Fch
            Xl_NominaItems1.DataSource = .Items
            Xl_AmtDevengat.Amt = .Devengat
            Xl_AmtDietes.Amt = .Dietes
            Xl_AmtSegSocial.Amt = .SegSocial
            Xl_AmtBaseIRPF.Amt = .IrpfBase
            Xl_AmtIRPF.Amt = .Irpf
            Xl_AmtEmbargos.Amt = .Embargos
            Xl_AmtDeutes.Amt = .Deutes
            Xl_IbanDigits1.Load(.IbanDigits)
        End With
    End Sub
End Class