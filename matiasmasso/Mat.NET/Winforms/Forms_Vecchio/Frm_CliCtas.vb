Public Class Frm_CliCtas
    Private _Contact As DTOContact
    Private _Cta As DTOPgcCta
    Private _Exercici As DTOExercici
    Private _SelectionMode As BLL.Defaults.SelectionModes

    Public Event AfterUpdate(sender As Object, e As EventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(oContact As DTOContact, Optional oCta As DTOPgcCta = Nothing, Optional oExercici As DTOExercici = Nothing, Optional oSelectionMode As BLL.Defaults.SelectionModes = BLL.Defaults.SelectionModes.Browse)
        MyBase.New()
        Me.InitializeComponent()

        _Contact = oContact

        If oCta Is Nothing Then oCta = oContact.DefaultCta
        _Cta = oCta

        If oExercici Is Nothing Then oExercici = BLL.BLLExercici.Current()
        _Exercici = oExercici

        _SelectionMode = oSelectionMode

        AddHandler Xl_Extracte1.requestToRefresh, AddressOf onRefreshRequest

    End Sub

    Private Sub Frm_CliCtas_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim oBackgroundWorker As New System.ComponentModel.BackgroundWorker
        AddHandler oBackgroundWorker.DoWork, AddressOf onDoWork
        AddHandler oBackgroundWorker.RunWorkerCompleted, AddressOf onRunWorkerCompleted
        oBackgroundWorker.RunWorkerAsync(_Contact)

        LoadCtas()
    End Sub

    Private Sub LoadCtas(Optional oDefaultExtracte As Extracte = Nothing)
        Dim oExercicis As List(Of DTOExercici) = BLL.BLLExercicis.All(_Contact)
        If oDefaultExtracte Is Nothing Then
            oDefaultExtracte = New Extracte(_Exercici, _Cta, _Contact)
        End If

        Xl_Exercicis1.Load(oExercicis, oDefaultExtracte.Exercici)

        Dim oExercici As Exercici = Xl_Exercicis1.SelectedItem
        If oExercici IsNot Nothing Then
            Dim oExtractes As Extractes = oExercici.Extractes
            Xl_Extractes1.Load(oExtractes, oDefaultExtracte)

            Dim oExtracte As Extracte = Xl_Extractes1.SelectedItem
            Xl_Extracte1.Load(oExtracte, _SelectionMode)
            Me.Show()
            Application.DoEvents()
        End If

    End Sub

    Private Sub onDoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs)
        Dim oContact As Contact = e.Argument

        If Me.Xl_Contact_Logo1.InvokeRequired Then
            Me.Invoke(Sub()
                          If _Contact Is Nothing Then
                              Me.Text = "Comptes sense subcompte"
                          Else
                              Me.Text = "Comptes de " & _Contact.Clx
                              Xl_Contact_Logo1.Contact = _Contact
                              Xl_Contact_Pnds1.Contact = _Contact
                          End If
                      End Sub)
        Else
            If _Contact Is Nothing Then
                Me.Text = "Comptes sense subcompte"
            Else
                Me.Text = "Comptes de " & _Contact.Clx
                Xl_Contact_Logo1.Contact = _Contact
                Xl_Contact_Pnds1.Contact = _Contact
            End If
        End If


    End Sub

    Private Sub onRunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs)
        If Me.Xl_Contact_Logo1.InvokeRequired Then
            Me.Invoke(Sub()
                          If _Contact IsNot Nothing Then Xl_Contact_Logo1.Visible = True
                      End Sub)
        Else
            If _Contact IsNot Nothing Then Xl_Contact_Logo1.Visible = True

        End If
    End Sub

    Private Sub Xl_Exercicis1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_Exercicis1.onItemSelected
        Dim oExercici As DTOExercici = e.Argument
        Dim oCtas As List(Of DTOPgcCta) = BLL.BLLPgcCtas.All(_Exercici, _Contact)
        Dim oDefaultCta As DTOPgcCta = Nothing
        Dim oDefaultCtaCod As DTOPgcPlan.Ctas = BLL.BLLPgcCtas.DefaultCtaCod(_Contact)
        If oDefaultCtaCod <> DTOPgcPlan.Ctas.NotSet Then
            oDefaultCta = oCtas.Find(Function(x) x.Cod = oDefaultCtaCod)
        End If
        Xl_PgcCtas1.Load(oCtas, oDefaultCta)
    End Sub

    Private Sub Xl_Extractes1_onItemSelected(sender As Object, e As MatEventArgs)
        Dim oExtracte As Extracte = e.Argument
        Xl_Extracte1.Load(oExtracte)
    End Sub

    Private Sub onRefreshRequest(sender As Object, e As EventArgs)
        Dim oPreviousExtracte As Extracte = Xl_Extractes1.SelectedItem
        LoadCtas(oPreviousExtracte)
    End Sub

    Private Sub Xl_Extracte1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_Extracte1.onItemSelected
        If _SelectionMode = BLL.Defaults.SelectionModes.Selection Then
            Dim oCcb As Ccb = e.Argument
            Dim oCca As Cca = oCcb.Cca
            RaiseEvent onItemSelected(Me, New MatEventArgs(oCca))
            Me.Close()
        End If
    End Sub
End Class