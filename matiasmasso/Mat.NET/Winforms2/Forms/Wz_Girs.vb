

Public Class Wz_Girs
    Private _Csbs As List(Of DTOCsb)
    Private _Bancs As List(Of DTOBanc)
    Private _DirtyEfts As Boolean
    Private _AllowEvents As Boolean
    Private BlDone As Boolean

    Private Async Sub Wz_Girs_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        DateTimePicker1.Value = DTO.GlobalVariables.Today()

        _Bancs = Await FEB.Bancs.AllActive(GlobalVariables.Emp, exs)
        If exs.Count = 0 Then
            _Csbs = Await FEB.Csbs.PendentsDeGirar(exs, GlobalVariables.Emp, oCountry:=DTOCountry.Wellknown(DTOCountry.Wellknowns.Spain), sepa:=True)
            If exs.Count = 0 Then
                Xl_Gir_SelEfts1.Load(_Csbs, New List(Of DTOCcd))
                Xl_Gir_SelBancs1.Load(Xl_Gir_SelEfts1.Values, _Bancs, DateTimePicker1.Value)
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs, "error al llegir els efectes pendents de cobro")
        End If

        _AllowEvents = True
    End Sub


    Private Async Function Grabacio() As Task
        Dim exs As New List(Of Exception)

        Dim oCsa As DTOCsa
        Dim oCsas As List(Of DTOCsa) = Xl_Gir_SelBancs1.Csas
        For Each oCsa In oCsas
            With oCsa
                .Descomptat = True
                If .Items.Count > 0 Then
                    .Items = .Items.OrderBy(Function(x) x.Contact.Nom).ToList
                End If
                oCsa.Cca = Await FEB.Csa.BuildCca(exs, oCsa, Current.Session.User)
            End With
        Next

        Dim caption As String = ""
        Dim ids = Await FEB.Csas.Update(oCsas, exs)
        If exs.Count = 0 Then
            If ids.Count = 1 Then
                caption = "remesa Sepa Core num." & ids.First
            Else
                caption = "remeses Sepa Core nums." & ids.First & "-" & ids.Last
            End If

            MsgBox(caption, MsgBoxStyle.Information, "MAT.NET")
            Me.Close()
        Else
            UIHelper.WarnError(exs)
        End If

    End Function


#Region "Wizard Common Events"
    Private Sub Wizard_AfterTabSelect()
        EnableNavButtons()
        Dim oTab As TabPage = TabControl1.SelectedTab
        Select Case oTab.Text
            Case TabPageCsas.Text
                If _DirtyEfts Then
                    'Xl_Gir_SelBancs1.Load(Xl_Gir_SelEfts1.Values, _Bancs)
                End If
        End Select
    End Sub


    Private Sub Wizard_NavigateNext(ByVal oPageSource As TabPage, Optional ByRef oPageTarget As TabPage = Nothing)
        Select Case oPageSource.Text
            Case TabPageCsbs.Text
        End Select
    End Sub

    Private Sub Wizard_NavigatePrevious(ByVal oPageSource As TabPage, Optional ByRef oPageTarget As TabPage = Nothing)
        Select Case oPageSource.Text
            'Case TabPageBancs.Text, TabPageVisas.Text
            'TabControl1.SelectedTab = TabPageCfp
            'Case TabPageEnd.Text
            'oPageTarget = CfpTabPage()
        End Select
    End Sub

    Private Async Function Wizard_NavigateEnd() As Task
        Await Grabacio()
    End Function
#End Region

#Region "Wizard Common Code"
    'Codi comú a totes les wizards
    'es recomana no modificar
    'aquest codi fa crides a la regió Wizard Common Events,
    'on hi va el codi propietari

    Private Sub TabControl1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabControl1.Click
        Wizard_AfterTabSelect()
    End Sub

    Private Sub ButtonPrevious_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonPrevious.Click
        Dim oPageTarget As TabPage = TabControl1.TabPages(TabControl1.SelectedIndex - 1)
        Wizard_NavigateNext(TabControl1.SelectedTab, oPageTarget)
        TabControl1.SelectedTab = oPageTarget
        Wizard_AfterTabSelect()
    End Sub

    Private Sub ButtonNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonNext.Click
        Dim oPageTarget As TabPage = TabControl1.TabPages(TabControl1.SelectedIndex + 1)
        Wizard_NavigateNext(TabControl1.SelectedTab, oPageTarget)
        TabControl1.SelectedTab = oPageTarget
        Wizard_AfterTabSelect()
    End Sub

    Private Async Sub ButtonEnd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonEnd.Click


        If BlDone Then
            Me.Close()
        Else
            BlDone = True
            Await Wizard_NavigateEnd()
            ButtonEnd.Text = "SORTIDA"
            ButtonPrevious.Enabled = False
        End If
    End Sub

    Private Sub EnableNavButtons()
        Dim Min As Integer = 0
        Dim Max As Integer = TabControl1.TabPages.Count - 1
        Dim Idx As Integer = TabControl1.SelectedIndex

        ButtonPrevious.Enabled = (Idx > Min)
        ButtonNext.Enabled = (Idx < Max)
        ButtonEnd.Enabled = (Idx = Max)
    End Sub

#End Region


    Private Sub Xl_Gir_SelEfts1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_Gir_SelEfts1.ValueChanged
        UpdateTotLabel()
        Xl_Gir_SelBancs1.Load(Xl_Gir_SelEfts1.Values, _Bancs, DateTimePicker1.Value)
        _DirtyEfts = True
    End Sub


    Private Sub Xl_AmountCur1_AfterUpdate(sender As Object, e As EventArgs) Handles Xl_AmountCur1.AfterUpdate
        Xl_Gir_SelEfts1.SelectAmount(Xl_AmountCur1.Amt)
        UpdateTotLabel()
        Xl_Gir_SelBancs1.Load(Xl_Gir_SelEfts1.Values, _Bancs, DateTimePicker1.Value)
    End Sub

    Private Sub UpdateTotLabel()
        Dim DcTot = Xl_Gir_SelEfts1.Values.Sum(Function(x) x.Amt.Eur)
        Dim oTot As DTOAmt = DTOAmt.Factory(DcTot)
        LabelTot.Text = oTot.Formatted
    End Sub
End Class
