Public Class Frm_Product_Historial
    Private _Product As DTOProduct
    Private _Value As Models.SkuInOutModel
    Private _Allowevents As Boolean

    Public Sub New(oProduct As DTOProduct)
        MyBase.New
        InitializeComponent()
        _Product = oProduct
    End Sub

    Private Async Sub Frm_Product_Historial_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB.Product.Load(_Product, exs) Then
            If TypeOf _Product Is DTOProductSku Then
                Dim sku = CType(_Product, DTOProductSku)
                Me.Text = String.Format("Historial de ({0}) {1}", sku.Id, sku.RefYNomLlarg().Tradueix(Current.Session.Lang))
                'Me.Text = String.Format("Historial de ({0}) {1}", DirectCast(_Product, DTOProductSku).Id, DTOProduct.GetNomLlargOrNom(_Product, Current.Session.Lang))
            Else
                Me.Text = String.Format("Historial de {0}", DTOProduct.GetNom(_Product))
            End If

            ProgressBar1.Visible = True
            _Value = Await FEB.DeliveryItems.All(exs, _Product)
            ProgressBar1.Visible = False

            If exs.Count = 0 Then
                LoadMgzs()
                refresca()
                _Allowevents = True
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub refresca()
        Dim oCurrentMgz = CurrentMgz()

        Dim items As List(Of Models.SkuInOutModel.Item)
        If oCurrentMgz Is Nothing Then
            items = _Value.Items.Where(Function(x) x.Mgz.Equals(Guid.Empty)).ToList
        Else
            items = _Value.Items.Where(Function(x) x.Mgz.Equals(oCurrentMgz.Guid)).ToList
        End If

        Dim isBundle As Boolean = False
        If TypeOf _Product Is DTOProductSku Then
            isBundle = CType(_Product, DTOProductSku).IsBundle
        ElseIf TypeOf _Product Is DTOProductCategory Then
            isBundle = CType(_Product, DTOProductCategory).IsBundle
        End If

        Xl_Product_Historial1.Load(Current.Session.User, items, isBundle, CurrentMode)
    End Sub

    Private Sub LoadMgzs()
        Dim oMgzs = _Value.Mgzs

        If _Value.Items.Any(Function(x) x.Mgz = Nothing) Then
            Dim oNoMgz = DTOGuidNom.Compact.Factory(Guid.Empty)
            oNoMgz.Nom = "(no especificat)"
            oMgzs.Add(oNoMgz)
        End If

        With ComboBoxMgz
            .DataSource = oMgzs
            .DisplayMember = "Nom"
            .SelectedItem = oMgzs.FirstOrDefault(Function(x) x.Equals(GlobalVariables.Emp.Mgz))
        End With
    End Sub

    Private Function CurrentMgz() As DTOGuidNom.Compact
        Return ComboBoxMgz.SelectedItem
    End Function

    Private Function CurrentMode()
        Dim retval As Xl_ProductHistorial.Modes = Xl_ProductHistorial.Modes.All
        If Not CheckBoxInp.Checked Then
            retval = Xl_ProductHistorial.Modes.Sortides
        ElseIf Not CheckBoxOut.Checked Then
            retval = Xl_ProductHistorial.Modes.Entrades
        End If
        Return retval
    End Function

    Private Sub ComboBoxMgz_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxMgz.SelectedIndexChanged
        If _Allowevents Then
            refresca()
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxInp.CheckedChanged
        If _Allowevents Then
            If CheckBoxInp.Checked = False Then CheckBoxOut.Checked = True
            refresca()
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxOut.CheckedChanged
        If _Allowevents Then
            If CheckBoxOut.Checked = False Then CheckBoxInp.Checked = True
            refresca()
        End If
    End Sub

    Private Sub Xl_TextboxSearch1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_TextboxSearch1.AfterUpdate
        Xl_Product_Historial1.Filter = e.Argument
    End Sub

    Private Sub Xl_Product_Historial1_RequestForExcel(sender As Object, e As MatEventArgs) Handles Xl_Product_Historial1.RequestForExcel
        Dim oFilteredValues = Xl_Product_Historial1.FilteredValues()
        Dim oSheet As MatHelper.Excel.Sheet = DTODeliveryItem.Excel(oFilteredValues, Me.Text)
        Dim exs As New List(Of Exception)
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub
End Class