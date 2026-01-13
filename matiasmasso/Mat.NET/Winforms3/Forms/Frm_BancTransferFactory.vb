Public Class Frm_BancTransferFactory
    Private _Mode As Modes
    Private _Pool As DTOBancTransferPool
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Enum Modes
        NotSet
        Reps
        Staff
        Traspas
    End Enum

    Public Sub New(Optional oMode As Modes = Modes.NotSet)
        MyBase.New
        InitializeComponent()
        _Mode = oMode
    End Sub

    Private Async Sub Frm_BancTransferFactory_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        If Await Xl_BancsComboBox1.LoadDefaultsFor(DTODefault.Codis.BancNominaTransfers, exs) Then
            Select Case _Mode
                Case Modes.Reps
                    Await LoadReps(exs)
                Case Modes.Staff
                    Await LoadStaff(exs)
                Case Else
                    LoadEmptyTransfer()
            End Select

            UIHelper.ToggleProggressBar(Panel1, False)
            If exs.Count = 0 Then
                ArxiuToolStripMenuItem.DropDownItems.Add("carrega representants", Nothing, AddressOf LoadReps)
                ArxiuToolStripMenuItem.DropDownItems.Add("carrega personal", Nothing, AddressOf LoadStaff)
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs)
        End If

    End Sub

    Private Sub LoadEmptyTransfer()
        'per que surti el menu afegir
        Dim oBeneficiaris As New List(Of DTOBancTransferBeneficiari)
        Xl_BancTransferBeneficiarisFactory1.Load(oBeneficiaris)
        SetTotal()
    End Sub

    Private Async Sub LoadReps()
        Dim exs As New List(Of Exception)
        Await LoadReps(exs)
    End Sub
    Private Async Function LoadReps(exs As List(Of Exception)) As Task
        _Pool = Await FEB.BancTransferPool.FromReps(Current.Session.User, exs)
        Xl_BancTransferBeneficiarisFactory1.Load(_Pool.Beneficiaris)
        SetTotal()
    End Function

    Private Async Sub LoadStaff()
        Dim exs As New List(Of Exception)
        Await LoadStaff(exs)
    End Sub

    Private Async Function LoadStaff(exs As List(Of Exception)) As Task
        _Pool = Await FEB.BancTransferPool.FromStaff(GlobalVariables.Emp, Current.Session.User, exs)
        Xl_BancTransferBeneficiarisFactory1.Load(_Pool.Beneficiaris)
        SetTotal()
    End Function

    Private Sub Xl_BancTransferBeneficiarisFactory1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_BancTransferBeneficiarisFactory1.AfterUpdate
        SetTotal()
    End Sub

    Private Sub Xl_BancTransferBeneficiarisFactory1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_BancTransferBeneficiarisFactory1.RequestToAddNew
        Dim value As New DTOBancTransferBeneficiari
        Dim oFrm As New Frm_BancTransferBeneficiari(value)
        AddHandler oFrm.AfterUpdate, AddressOf onValueAdded
        oFrm.Show()
    End Sub

    Private Sub onValueAdded(sender As Object, e As MatEventArgs)
        Dim value As DTOBancTransferBeneficiari = e.Argument
        Dim values As List(Of DTOBancTransferBeneficiari) = Xl_BancTransferBeneficiarisFactory1.Values
        values.Add(value)
        Xl_BancTransferBeneficiarisFactory1.Load(values)
        SetTotal()
    End Sub

    Private Sub SetTotal()
        Dim items As List(Of DTOBancTransferBeneficiari) = Xl_BancTransferBeneficiarisFactory1.Values
        Dim iCount As Integer = items.Count
        Dim DcTot As Decimal = items.Sum(Function(x) x.Amt.Eur)
        LabelTot.Text = String.Format("Total {0} beneficiaris per import de {1:#,###.00}€", iCount, DcTot)
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        UIHelper.ToggleProggressBar(Panel1, True)
        Dim oPool As DTOBancTransferPool = DTOBancTransferPool.Factory(
            Current.Session.User,
            DateTimePicker1.Value,
            Xl_BancsComboBox1.SelectedItem)

        oPool.Beneficiaris = Xl_BancTransferBeneficiarisFactory1.Values
        Dim exs As New List(Of Exception)
        Dim CcaId = Await FEB.BancTransferPool.Save(exs, oPool)
        If exs.Count = 0 Then
            oPool.Cca.id = CcaId
            RaiseEvent AfterUpdate(Me, New MatEventArgs(oPool))
            Dim XMLSource As String = Await FEB.SepaCreditTransfer.XML(Current.Session.Emp, oPool, exs)
            If exs.Count = 0 Then
                Dim sFilename As String = oPool.DefaultFilename()
                UIHelper.SaveXmlFileDialog(XMLSource, sFilename)
                Me.Close()
            Else
                UIHelper.ToggleProggressBar(Panel1, False)
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs)
        End If
    End Sub


End Class