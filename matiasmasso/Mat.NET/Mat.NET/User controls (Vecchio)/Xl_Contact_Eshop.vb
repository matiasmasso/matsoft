
Imports System.Data.SqlClient

Public Class Xl_Contact_Eshop
    Public Event AfterUpdate()

    Private meShop As eShop
    Private mDsTpas As DataSet
    Private mAllowEvents As Boolean

    Public Property eShop() As eShop
        Get
            If meShop IsNot Nothing Then
                With meShop
                    .Nom = TextBoxNom.Text
                    .Web = TextBoxWeb.Text
                    .Logo = Xl_ImageLogo.Bitmap
                    .Obsoleto = Not CheckBoxActivated.Checked
                    .Integrat = CheckBoxIntegrat.Checked
                End With
            End If
            Return meShop
        End Get
        Set(ByVal value As eShop)
            If value IsNot Nothing Then
                meShop = value
                DisplayData()
                mAllowEvents = True
            End If
        End Set
    End Property

    Public ReadOnly Property Activated() As Boolean
        Get
            Return CheckBoxActivated.Checked
        End Get
    End Property

    Private Sub DisplayData()
        LoadTpas()
        With meShop
            TextBoxNom.Text = .Nom
            TextBoxWeb.Text = .Web
            Xl_ImageLogo.Bitmap = .Logo
            CheckBoxActivated.Checked = Not .Obsoleto
            GroupBoxActivated.Enabled = CheckBoxActivated.Checked
            CheckBoxIntegrat.Checked = .Integrat
            LabelGuid.Text = MaxiSrvr.Contact.FromNum(.Emp, .Id).Guid.ToString
        End With

        Dim oTbTpa As DataTable = mDsTpas.Tables(0)

        Dim BlFirstItemSelected As Boolean
        For i As Integer = 0 To CheckedListBoxTpa.Items.Count - 1
            For Each oItm As eShopTpa In meShop.eShopTpas
                If CInt(oTbTpa.Rows(i)("TPA")) = oItm.Tpa.Id Then
                    If Not BlFirstItemSelected Then
                        CheckedListBoxTpa.SelectedIndex = i
                        LoadPunts(oItm.Tpa)
                        CheckedListBoxPunts.Visible = True
                        BlFirstItemSelected = True
                    End If
                    CheckedListBoxTpa.SetItemChecked(i, True)
                    Exit For
                End If
            Next
        Next
    End Sub

    Private Sub LoadTpas()
        Dim SQL As String = "SELECT TPA,DSC FROM TPA WHERE " _
        & "EMP=" & CInt(meShop.Emp.Id).ToString & " AND " _
        & "OBSOLETO=0 " _
        & "ORDER BY ORD"

        mDsTpas = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = mDsTpas.Tables(0)
        For Each oRow As DataRow In oTb.Rows
            CheckedListBoxTpa.Items.Add(oRow("DSC"))
        Next
    End Sub

    Private Function CurrentTpa() As Tpa
        Dim oTpa As Tpa = Nothing
        If CheckedListBoxTpa.SelectedIndex >= 0 Then
            Dim iTpa As Integer = mDsTpas.Tables(0).Rows(CheckedListBoxTpa.SelectedIndex)("TPA")
            oTpa = New Tpa(meShop.Emp, iTpa)
        End If
        Return oTpa
    End Function

    Private Function CurrentEShopTpa() As eShopTpa
        Dim oEShopTpa As eShopTpa = Nothing
        Dim oTpa As Tpa = CurrentTpa()
        For Each oItm As eShopTpa In meShop.eShopTpas
            If oTpa.Id = oItm.Tpa.Id Then
                oEShopTpa = oItm
                Exit For
            End If
        Next
        Return oEShopTpa
    End Function

    Private Sub CheckedListBoxTpa_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles CheckedListBoxTpa.ItemCheck
        If mAllowEvents Then
            Select Case e.NewValue
                Case CheckState.Checked
                    CheckedListBoxTpa.SelectedIndex = e.Index
                    Dim oEShopTpa As New eShopTpa(CurrentTpa)
                    meShop.eShopTpas.Add(oEShopTpa)
                    CheckedListBoxPunts.Visible = True
                Case Else
                    Dim oTpa As Tpa = CurrentTpa()
                    For i As Integer = 0 To meShop.eShopTpas.Count - 1
                        If oTpa.Id = meShop.eShopTpas(i).Tpa.Id Then
                            meShop.eShopTpas.Remove(i)
                            Exit For
                        End If
                    Next
                    CheckedListBoxPunts.Visible = False
            End Select
            RaiseEvent AfterUpdate()
        End If

    End Sub

 
    Private Sub CheckedListBoxTpa_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckedListBoxTpa.SelectedIndexChanged
        If CheckedListBoxTpa.GetItemChecked(CheckedListBoxTpa.SelectedIndex) Then
            CheckedListBoxPunts.Visible = True
            LoadPunts(CurrentTpa)
        Else
            TextBoxTpa.Text = ""
            CheckedListBoxPunts.Visible = False
        End If
    End Sub

    Private Sub LoadPunts(ByVal oTpa As Tpa)
        TextBoxTpa.Text = oTpa.Nom
        Dim oEShopTpa As eShopTpa = CurrentEShopTpa()
        If oEShopTpa Is Nothing Then
            mAllowEvents = False
            For i As Integer = 0 To CheckedListBoxPunts.Items.Count - 1
                CheckedListBoxPunts.SetItemChecked(i, 0)
            Next
            mAllowEvents = True
        Else
            TextBoxLink.Text = oEShopTpa.DirectLink
            For i As Integer = 0 To CheckedListBoxPunts.Items.Count - 1
                CheckedListBoxPunts.SetItemChecked(i, (oEShopTpa.Punt(i) = 1))
            Next
        End If
    End Sub

    Private Sub CheckedListBoxPunts_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles CheckedListBoxPunts.ItemCheck
        If mAllowEvents Then
            CurrentEShopTpa.Punt(e.Index) = IIf(e.NewValue = CheckState.Checked, 1, 0)
            RaiseEvent AfterUpdate()
        End If
    End Sub

    Private Sub CheckBoxActivated_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxActivated.CheckedChanged
        GroupBoxActivated.Enabled = CheckBoxActivated.Checked
        meShop.Obsoleto = Not CheckBoxActivated.Checked
    End Sub

    Private Sub Control_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    Xl_ImageLogo.AfterUpdate, _
     TextBoxNom.TextChanged, _
      TextBoxWeb.TextChanged
        RaiseEvent AfterUpdate()
    End Sub

    Private Sub TextBoxLink_TextChanged(sender As Object, e As System.EventArgs) Handles TextBoxLink.TextChanged
        If mAllowEvents Then
            CurrentEShopTpa.DirectLink = TextBoxLink.text
            RaiseEvent AfterUpdate()
        End If
    End Sub
End Class
