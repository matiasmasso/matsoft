Public Class Frm_Extracte

    Private _Contact As DTOContact
    Private _Cta As DTOPgcCta
    Private _Exercici As DTOExercici
    Private _DefaultCtaCod As DTOPgcPlan.Ctas
    Private _SelectionMode As DTO.Defaults.SelectionModes
    Private _Modalitat As Modalitats = Modalitats.SingleContact
    Private TabPndsLoaded As Boolean


    Public Event AfterUpdate(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Tabs
        General
        Pnds
    End Enum

    Private Enum Modalitats
        SingleContact
        SingleAccount
    End Enum

    Public Sub New(oContact As DTOContact, Optional oCta As DTOPgcCta = Nothing, Optional oExercici As DTOExercici = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        MyBase.New()
        Me.InitializeComponent()
        _Contact = oContact
        _Cta = oCta
        _Exercici = oExercici
        _SelectionMode = oSelectionMode
        _Modalitat = IIf(_Contact IsNot Nothing, Modalitats.SingleContact, Modalitats.SingleAccount)
    End Sub

    Private Async Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        Select Case _Modalitat
            Case Modalitats.SingleAccount
                TabControl1.TabPages(Tabs.Pnds).Enabled = False
                Me.Text = "Compte " & DTOPgcCta.FullNom(_Cta, Current.Session.Lang)
                Dim oXl_Contacts As New Xl_Contacts
                AddHandler oXl_Contacts.ValueChanged, AddressOf onContactChanged
                oXl_Contacts.Dock = DockStyle.Fill
                With SplitContainer2.Panel1
                    .Controls.Remove(Xl_PgcCtas1)
                    .Controls.Add(oXl_Contacts)
                End With

                Dim oExercicis = Await FEB2.Exercicis.All(exs, Current.Session.Emp, oCta:=CurrentCta)
                If exs.Count = 0 Then
                    If oExercicis.Count = 0 Then
                        MsgBox("no hi han comptes registrades a aquest nom", MsgBoxStyle.Information, "Mat.NET")
                        Me.Close()
                    Else
                        Xl_Exercicis1.Load(oExercicis, _Exercici)

                        Dim oContacts As New List(Of DTOContact)
                        If CurrentExercici() IsNot Nothing Then
                            oContacts = Await FEB2.Contacts.All(exs, CurrentExercici, _Cta)
                            If exs.Count > 0 Then
                                UIHelper.WarnError(exs)
                            End If
                        End If

                        oXl_Contacts.Load(oContacts)
                        SplitContainer2.Panel1Collapsed = oContacts.Count = 0

                        Await LoadCcbs()
                    End If
                Else
                    UIHelper.WarnError(exs)
                End If

            Case Modalitats.SingleContact
                FEB2.Contact.Load(_Contact, exs)
                Me.Text = "Comptes de " & _Contact.FullNom
                _DefaultCtaCod = DTOContact.DefaultCtaCod(_Contact)

                Dim oExercicis = Await FEB2.Exercicis.All(exs, Current.Session.Emp, CurrentContact)
                If exs.Count = 0 Then
                    If oExercicis.Count = 0 Then
                        MsgBox("no hi han comptes registrades a aquest nom", MsgBoxStyle.Information, "Mat.NET")
                        Me.Close()
                    Else
                        Xl_Exercicis1.Load(oExercicis, _Exercici)

                        Await LoadCtas()
                        Await LoadCcbs()
                    End If
                Else
                    UIHelper.WarnError(exs)
                End If

        End Select

    End Sub


    Private Async Function LoadCtas() As Task
        Dim exs As New List(Of Exception)
        Dim oCtas = Await FEB2.PgcCtas.All(exs, CurrentExercici, CurrentContact)
        If exs.Count = 0 Then
            If _Cta Is Nothing Then _Cta = oCtas.Find(Function(x) x.Codi = _DefaultCtaCod)
            Xl_PgcCtas1.Load(oCtas, _Cta)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Sub LoadCcbs(sender As Object, e As MatEventArgs)
        Await LoadCcbs()
    End Sub

    Private Async Function LoadCcbs() As Task
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        Dim oCcbs = Await FEB2.Ccbs.All(exs, CurrentExercici, CurrentCta, CurrentContact)
        ProgressBar1.Visible = False
        If exs.Count = 0 Then
            Xl_Extracte_Ccbs1.Load(oCcbs, _SelectionMode)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Sub Xl_Exercicis1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_Exercicis1.ValueChanged
        Dim exs As New List(Of Exception)
        Select Case _Modalitat
            Case Modalitats.SingleAccount
                Dim oSplitPanel As Panel = SplitContainer2.Panel1
                Dim oXl_Contacts As Xl_Contacts = oSplitPanel.Controls(0)
                Dim oExercici As DTOExercici = CurrentExercici()
                Dim oContacts = Await FEB2.Contacts.All(exs, oExercici, _Cta)
                If exs.Count = 0 Then
                    oXl_Contacts.Load(oContacts)
                    Await LoadCcbs()
                Else
                    UIHelper.WarnError(exs)
                End If
            Case Modalitats.SingleContact
                Await LoadCtas()
                Await LoadCcbs()
        End Select

    End Sub


    Private Async Sub Xl_PgcCtas1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_PgcCtas1.ValueChanged
        Await LoadCcbs()
    End Sub

    Private Async Sub onContactChanged(sender As Object, e As MatEventArgs)
        Dim oContact As DTOContact = e.Argument
        Dim exs As New List(Of Exception)
        Dim oCcbs = Await FEB2.Ccbs.All(exs, CurrentExercici, _Cta, oContact)
        If exs.Count = 0 Then
            Xl_Extracte_Ccbs1.Load(oCcbs, _SelectionMode)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub TextBoxSearch_TextChanged(sender As Object, e As EventArgs) Handles TextBoxSearch.TextChanged
        Dim sSearchKey As String = TextBoxSearch.Text
        If sSearchKey.Length > 1 Then
            TextBoxSearch.ForeColor = Color.Black
            Xl_Extracte_Ccbs1.Filter = sSearchKey
            Xl_Pnds1.Filter = sSearchKey
        Else
            Xl_Extracte_Ccbs1.ClearFilter()
            Xl_Pnds1.ClearFilter()
            TextBoxSearch.ForeColor = Color.Gray
        End If

    End Sub

    Private Sub Xl_Extracte_Ccbs1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Extracte_Ccbs1.RequestToAddNew
        Dim oCca As DTOCca = DTOCca.Factory(Today, Current.Session.User, DTOCca.CcdEnum.Manual)
        oCca.AddCredit(DTOAmt.Empty, CurrentCta, CurrentContact)
        Dim oFrm As New Frm_Cca(oCca)
        AddHandler oFrm.AfterUpdate, AddressOf LoadCcbs
        oFrm.Show()
    End Sub

    Private Function SumPnds(oPnds As List(Of DTOPnd)) As DTOAmt
        Dim retval As DTOAmt
        Dim DcDeb As Decimal = oPnds.Where(Function(x) x.Cod = DTOPnd.Codis.Deutor).Sum(Function(x) x.Amt.Eur)
        Dim DcHab As Decimal = oPnds.Where(Function(x) x.Cod = DTOPnd.Codis.Creditor).Sum(Function(x) x.Amt.Eur)
        Select Case _Contact.rol.id
            Case DTORol.Ids.Manufacturer
                retval = DTOAmt.Factory(DcHab - DcDeb)
            Case Else
                retval = DTOAmt.Factory(DcDeb - DcHab)
        End Select
        Return retval
    End Function


    Private Async Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        Select Case TabControl1.SelectedIndex
            Case Tabs.Pnds
                If Not TabPndsLoaded Then
                    Await RefrescaPnds()
                    TabPndsLoaded = True
                End If
        End Select
    End Sub

    Private Sub Xl_Pnds1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Pnds1.RequestToAddNew
        Dim oPnd = DTOPnd.Factory(Current.Session.Emp)
        With oPnd
            .Contact = _Contact
            .Fch = Today
            .Vto = Today
            .Amt = DTOAmt.Empty
        End With

        Dim oFrm As New Frm_Contact_Pnd(oPnd)
        AddHandler oFrm.AfterUpdate, AddressOf RefrescaPnds
        oFrm.Show()
    End Sub

    Private Async Sub Xl_Pnds1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Pnds1.RequestToRefresh
        Await RefrescaPnds()
    End Sub

    Private Async Sub RefrescaPnds(sender As Object, e As MatEventArgs)
        Await RefrescaPnds()
    End Sub

    Private Async Function RefrescaPnds() As Task
        Dim exs As New List(Of Exception)
        If _Contact IsNot Nothing Then
            Dim oPnds = Await FEB2.Pnds.All(exs, Current.Session.Emp, _Contact)
            Dim oImpagats = Await FEB2.Impagats.All(exs, Current.Session.Emp, _Contact)
            If exs.Count = 0 Then
                oImpagats = oImpagats.FindAll(Function(x) x.Status <> DTOImpagat.StatusCodes.Saldat And x.Status <> DTOImpagat.StatusCodes.Insolvencia)
                Dim DcNominalImpagats = oImpagats.Sum(Function(x) x.Nominal.Eur)
                Dim dcDespeses = oImpagats.Sum(Function(x) x.Gastos.Eur)
                Dim dcPagatACompte = oImpagats.Sum(Function(x) x.PagatACompte.Eur)
                Dim dcPnds = SumPnds(oPnds).eur
                LabelSumPnds.Text = "Pendent de liquidar: " & DTOAmt.CurFormatted(dcPnds)
                If DcNominalImpagats <> 0 Then
                    LabelSumPnds.Text += String.Format(" + {0} en impagats", DTOAmt.CurFormatted(DcNominalImpagats))
                End If
                If dcDespeses <> 0 Then
                    LabelSumPnds.Text += String.Format(" + {0} de despeses", DTOAmt.CurFormatted(dcDespeses))
                End If
                If dcPagatACompte <> 0 Then
                    LabelSumPnds.Text += String.Format(" - {0} pagat a compte", DTOAmt.CurFormatted(dcPagatACompte))
                End If
                If DcNominalImpagats + dcDespeses - dcPagatACompte <> 0 Then
                    LabelSumPnds.Text += String.Format(" total {0}", DTOAmt.CurFormatted(dcPnds + DcNominalImpagats + dcDespeses - dcPagatACompte))
                End If
                Xl_Pnds1.Load(oPnds, oImpagats)
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Function

    Private Async Sub Xl_Extracte_Ccbs1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Extracte_Ccbs1.RequestToRefresh
        Await LoadCcbs()
    End Sub

    Private Function CurrentContact() As DTOContact
        Dim retval As DTOContact = _Contact
        Select Case _Modalitat
            Case Modalitats.SingleContact
                retval = _Contact
            Case Modalitats.SingleAccount
                Dim oSplitPanel As Panel = SplitContainer2.Panel1
                Dim oXl_Contacts As Xl_Contacts = oSplitPanel.Controls(0)
                retval = oXl_Contacts.Value
        End Select
        Return retval
    End Function


    Private Function CurrentCta() As DTOPgcCta
        Dim retval As DTOPgcCta = Nothing
        Select Case _Modalitat
            Case Modalitats.SingleContact
                retval = Xl_PgcCtas1.Value
            Case Modalitats.SingleAccount
                retval = _Cta
        End Select
        Return retval
    End Function

    Private Function CurrentExercici() As DTOExercici
        Dim retval As DTOExercici = Xl_Exercicis1.Value
        Return retval
    End Function

    Private Sub TabControl1_Selecting(sender As Object, e As TabControlCancelEventArgs) Handles TabControl1.Selecting
        If e.TabPageIndex = Tabs.Pnds And _Contact Is Nothing Then
            e.Cancel = True
        End If
    End Sub

    Private Sub Xl_Extracte_Ccbs1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_Extracte_Ccbs1.onItemSelected
        If _SelectionMode = DTO.Defaults.SelectionModes.Selection Then
            RaiseEvent onItemSelected(Me, e)
            Me.Close()
        End If
    End Sub
End Class