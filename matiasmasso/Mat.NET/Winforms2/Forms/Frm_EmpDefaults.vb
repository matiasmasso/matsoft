Public Class Frm_EmpDefaults
    Private _AllowEvents As Boolean

    'Private _defaults As List(Of DTODefault)
    Private Async Sub Frm_EmpDefaults_Load(sender As Object, e As EventArgs) Handles Me.Load
        '_defaults = BLLDefaults.All(Current.session.emp)
        Dim exs As New List(Of Exception)

        If Await Xl_BancsComboBoxTpv.LoadDefaultsFor(DTODefault.Codis.BancTpv, exs) Then
            Xl_LookupPaymentGatewayProduccio.PaymentGateway = Await FEB.PaymentGateway.ProductionEnvironment(exs, Current.Session.Emp)
            Xl_LookupPaymentGatewayProves.PaymentGateway = Await FEB.PaymentGateway.TestingEnvironment(exs, Current.Session.Emp)
            Xl_PercentImpago.Value = Await FEB.Default.EmpDecimal(Current.Session.Emp, DTODefault.Codis.DespesesImpago, exs)
            Xl_EurMinimImpago.Amt = Await FEB.Default.EmpAmt(Current.Session.Emp, DTODefault.Codis.DespesesImpagoMinim, exs)
            CheckBoxMatSchedLogLevelIntensive.Checked = Await FEB.Default.EmpBoolean(Current.Session.Emp, DTODefault.Codis.MatschedLogLevel, exs)
            ComboBoxMatschedLogLevel.SelectedIndex = Await FEB.Default.EmpInteger(Current.Session.Emp, DTODefault.Codis.MatschedLogLevel, exs)
            NumericUpDownMatschedInterval.Value = Await FEB.Default.EmpInteger(Current.Session.Emp, DTODefault.Codis.MatschedTaskInterval, exs)
            TextBoxEdiversaPath.Text = Await FEB.Default.EmpValue(Current.Session.Emp, DTODefault.Codis.eDiversaPath, exs)
            If exs.Count = 0 Then
                If Await Xl_BancsComboBoxXNomines.LoadDefaultsFor(DTODefault.Codis.BancNominaTransfers, exs) Then
                    Xl_ContactMgz.Contact = GlobalVariables.Emp.Mgz
                    Xl_ContactTaller.Contact = Current.Session.Emp.Taller
                    Xl_PercentDtoTarifa.Value = Await FEB.Default.EmpDecimal(Current.Session.Emp, DTODefault.Codis.DtoTarifa, exs)
                    Xl_ContactSpvTrp.Contact = Await FEB.Default.Contact(Current.Session.Emp, DTODefault.Codis.SpvTrp, exs)
                    _AllowEvents = True
                Else
                    UIHelper.WarnError(exs)
                End If
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub



    Private Async Sub Xl_LookupPaymentGatewayProduccio1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_LookupPaymentGatewayProduccio.AfterUpdate
        Dim exs As New List(Of Exception)
        If Not Await FEB.Default.SetEmpValue(Current.Session.Emp, DTODefault.Codis.SermepaTpvProductionEnvironment, Xl_LookupPaymentGatewayProduccio.PaymentGateway.Guid.ToString, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Xl_LookupPaymentGatewayProves1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_LookupPaymentGatewayProves.AfterUpdate
        Dim exs As New List(Of Exception)
        If Not Await FEB.Default.SetEmpValue(Current.Session.Emp, DTODefault.Codis.SermepaTpvTestingEnvironment, Xl_LookupPaymentGatewayProves.PaymentGateway.Guid.ToString, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Xl_PercentImpago_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_PercentImpago.AfterUpdate
        Dim exs As New List(Of Exception)
        If Not Await FEB.Default.SetEmpValue(Current.Session.Emp, DTODefault.Codis.DespesesImpago, Xl_PercentImpago.Value, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub
    Private Async Sub Xl_EurMinimImpago_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_EurMinimImpago.AfterUpdate
        Dim exs As New List(Of Exception)
        If Not Await FEB.Default.SetEmpValue(Current.Session.Emp, DTODefault.Codis.DespesesImpagoMinim, Xl_EurMinimImpago.Amt, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub CheckBoxMatSchedLogLevelIntensive_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxMatSchedLogLevelIntensive.CheckedChanged
        Dim exs As New List(Of Exception)
        If Not Await FEB.Default.SetEmpBoolean(Current.Session.Emp, DTODefault.Codis.MatschedLogLevel, CheckBoxMatSchedLogLevelIntensive.Checked, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub TextBoxEdiversaPath_Validated(sender As Object, e As EventArgs) Handles TextBoxEdiversaPath.Validated
        Dim exs As New List(Of Exception)
        If Not Await FEB.Default.SetEmpValue(Current.Session.Emp, DTODefault.Codis.eDiversaPath, TextBoxEdiversaPath.Text, exs) Then
            UIHelper.WarnError(exs)
        End If

    End Sub

    Private Async Sub Xl_BancsComboBoxXNomines_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_BancsComboBoxXNomines.ValueChanged
        Dim exs As New List(Of Exception)
        If Not Await FEB.Default.SetEmpValue(Current.Session.Emp, DTODefault.Codis.BancNominaTransfers, e.Argument, exs) Then
            UIHelper.WarnError(exs)
        End If

    End Sub

    Private Async Sub Xl_BancsComboBoxTpv_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_BancsComboBoxTpv.ValueChanged
        Dim exs As New List(Of Exception)
        If Not Await FEB.Default.SetEmpValue(Current.Session.Emp, DTODefault.Codis.BancTpv, e.Argument, exs) Then
            UIHelper.WarnError(exs)
        End If

    End Sub

    Private Async Sub ComboBoxMatschedLogLevel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxMatschedLogLevel.SelectedIndexChanged
        If _AllowEvents Then
            Dim exs As New List(Of Exception)
            If Not Await FEB.Default.SetEmpValue(Current.Session.Emp, DTODefault.Codis.MatschedLogLevel, ComboBoxMatschedLogLevel.SelectedIndex, exs) Then
                UIHelper.WarnError(exs)
            End If

        End If
    End Sub

    Private Async Sub Xl_ContactMgz_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_ContactMgz.AfterUpdate
        Dim oMgz As DTOMgz = e.Argument
        Dim exs As New List(Of Exception)
        If Not Await FEB.Default.SetEmpValue(Current.Session.Emp, DTODefault.Codis.Mgz, oMgz, exs) Then
            UIHelper.WarnError(exs)
        End If

    End Sub

    Private Async Sub Xl_ContactTaller_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_ContactTaller.AfterUpdate
        Dim oTaller As DTOTaller = e.Argument
        Dim exs As New List(Of Exception)
        If Not Await FEB.Default.SetEmpValue(Current.Session.Emp, DTODefault.Codis.Mgz, oTaller, exs) Then
            UIHelper.WarnError(exs)
        End If

    End Sub

    Private Async Sub NumericUpDownMatschedInterval_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDownMatschedInterval.ValueChanged
        Dim exs As New List(Of Exception)
        If Not Await FEB.Default.SetEmpValue(Current.Session.Emp, DTODefault.Codis.MatschedTaskInterval, NumericUpDownMatschedInterval.Value, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Xl_PercentDtoTarifa_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_PercentDtoTarifa.AfterUpdate
        Dim exs As New List(Of Exception)
        If Not Await FEB.Default.SetEmpValue(Current.Session.Emp, DTODefault.Codis.DtoTarifa, Xl_PercentDtoTarifa.Value, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Xl_ContactSpvTrp_Load(sender As Object, e As EventArgs) Handles Xl_ContactSpvTrp.AfterUpdate
        Dim exs As New List(Of Exception)
        If Xl_ContactSpvTrp.Contact Is Nothing Then
            If Not Await FEB.Default.SetEmpValue(Current.Session.Emp, DTODefault.Codis.SpvTrp, "", exs) Then
                UIHelper.WarnError(exs)
            End If
        Else
            If Not Await FEB.Default.SetEmpValue(Current.Session.Emp, DTODefault.Codis.SpvTrp, Xl_ContactSpvTrp.Contact.Guid.ToString, exs) Then
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub
End Class