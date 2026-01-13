Public Class Frm_OutVivaceExpedicio

    Private _value As DTOOutVivaceLog.expedicion

    Public Sub New(value As DTOOutVivaceLog.expedicion)
        MyBase.New
        InitializeComponent()
        _value = value
    End Sub

    Private Sub Frm_OutVivaceExpedicio_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Text = String.Format("Expedició {0}", _value.vivace)

        With _value
            TextBoxId.Text = .vivace
            TextBoxFch.Text = .log.fecha
            TextBoxCliNom.Text = BllOutVivaceExpedicio.Destinacio(_value)
            If .Trp Is Nothing Then
                TextBoxTrpNom.Text = .transpnif
            Else
                TextBoxTrpNom.Text = .Trp.Nom
            End If
            If .bultos <> 0 Then
                TextBoxBultos.Text = .bultos
            End If

            If .m3 <> 0 Then
                TextBoxM3.Text = String.Format("{0:0.000} m3", .m3)
            End If
            If .kg <> 0 Then
                TextBoxKg.Text = String.Format("{0} Kg", Math.Round(.kg))
            End If

            Dim oValor As DTOAmt = BllOutVivaceExpedicio.Valor(_value)
            TextBoxValor.Text = DTOAmt.CurFormatted(oValor)

            Dim oCostLogistic As DTOAmt = BllOutVivaceExpedicio.CostLogistic(_value)
            TextBoxCostLogistic.Text = DTOAmt.CurFormatted(oCostLogistic)

            If oValor.Eur > 0 Then
                TextBoxRate.Text = String.Format("{0,00}%", Math.Round(100 * oCostLogistic.Eur / oValor.Eur, 2))
            End If
            LabelFchCreated.Text = String.Format("(registrat el {0:dd/MM/yy HH:mm})", .log.fchCreated)
        End With

        Dim items As List(Of DTOOutVivaceLog.cargo) = _value.cargos
        For Each oAlb As DTOOutVivaceLog.albaran In _value.albaranes
            items.AddRange(oAlb.cargos)
        Next

        Xl_OutVivaceCarrecs1.Load(items)
    End Sub

End Class