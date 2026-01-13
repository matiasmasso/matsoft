Public Class Frm_CliProductBlocked

    Private _CliProductBlocked As DTOCliProductBlocked
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oCliProductBlocked As DTOCliProductBlocked)
        MyBase.New()
        Me.InitializeComponent()
        _CliProductBlocked = oCliProductBlocked
    End Sub

    Private Async Sub Frm_CliProductBlocked_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If _CliProductBlocked.Contact IsNot Nothing AndAlso _CliProductBlocked.Product IsNot Nothing Then
            If FEB.CliProductBlocked.Load(exs, _CliProductBlocked) Then
                Await Refresca()
                _AllowEvents = True
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            Await Refresca()
            _AllowEvents = True
        End If
    End Sub


    Private Async Function Refresca() As Task

        With _CliProductBlocked
            TextBoxCliNom.Text = .Contact.FullNom
            If .Product Is Nothing Then
                PanelDetall.Visible = False
                'Xl_LookupProduct1.Load(BLL_App.Catalog, Nothing)
            Else
                PanelDetall.Visible = True

                'PictureBoxLogo.Image = BLL_Product.Brand.Product.Tpa.Image
                Dim oProducts As New List(Of DTOProduct)
                If .product IsNot Nothing Then oProducts.Add(.product)
                Xl_LookupProduct1.Load(oProducts, DTOProduct.SelectionModes.SelectAny)
                Await LoadClientsAmbExclusiva()
                TextBoxObs.Text = .Obs

                Dim oProduct As DTOProduct = Xl_LookupProduct1.Product
                EnableCheckboxes(oProduct)

                Select Case .Cod
                    Case DTOCliProductBlocked.Codis.Standard
                        RadioButtonStandard.Checked = True
                    Case DTOCliProductBlocked.Codis.DistribuidorOficial
                        RadioButtonDistribuidorOficial.Checked = True
                    Case DTOCliProductBlocked.Codis.Exclusiva
                        RadioButtonExclusiva.Checked = True
                        TextBoxZip.Text = _CliProductBlocked.Zip
                    Case DTOCliProductBlocked.Codis.Exclos
                        RadioButtonExclos.Checked = True
                    Case DTOCliProductBlocked.Codis.NoAplicable
                        RadioButtonNA.Checked = True
                    Case DTOCliProductBlocked.Codis.AltresEnExclusiva
                        RadioButtonAltresExclusiva.Checked = True
                End Select
            End If
        End With

        EnableControls()
    End Function

    Private Async Function LoadClientsAmbExclusiva() As Task
        Dim exs As New List(Of Exception)
        Dim oContacts = Await FEB.CliProductBlocked.AltresEnExclusiva(exs, _CliProductBlocked.Contact, _CliProductBlocked.Product)
        If exs.Count = 0 Then
            With ListBoxClis
                .DisplayMember = "Nom"
                .ValueMember = "Guid"
                .DataSource = oContacts
            End With
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Sub EnableControls()
        Dim BlEnableWarn As Boolean = ListBoxClis.Items.Count > 0

        If _CliProductBlocked.Product IsNot Nothing Then
            Dim exs As New List(Of Exception)
            FEB.Product.Load(_CliProductBlocked.Product, exs)
            If FEB.Product.BrandCodDist(_CliProductBlocked.Product) = DTOProductBrand.CodDists.Free Then
                PictureBoxStandard.Enabled = Not BlEnableWarn
                RadioButtonStandard.Enabled = Not BlEnableWarn
                TextBoxLabelStandard.Enabled = Not BlEnableWarn

                PictureBoxDistribuidorOficial.Enabled = Not BlEnableWarn
                RadioButtonDistribuidorOficial.Enabled = Not BlEnableWarn

                PictureBoxAltresExclusiva.Enabled = BlEnableWarn
                RadioButtonAltresExclusiva.Enabled = BlEnableWarn

                TextBoxZip.Enabled = RadioButtonExclusiva.Checked

            End If
        End If

    End Sub

    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        RadioButtonExclos.CheckedChanged,
        RadioButtonNA.CheckedChanged,
        RadioButtonExclusiva.CheckedChanged,
        RadioButtonDistribuidorOficial.CheckedChanged,
        TextBoxZip.TextChanged,
        RadioButtonAltresExclusiva.CheckedChanged,
        RadioButtonStandard.CheckedChanged,
        TextBoxObs.TextChanged

        If _AllowEvents Then
            ButtonOk.Enabled = True
            EnableControls()
        End If
    End Sub

    Private Async Sub ButtonOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With _CliProductBlocked
            .Product = Xl_LookupProduct1.Product
            If .Product Is Nothing Then
                MsgBox("cal triar una marca o producte", MsgBoxStyle.Exclamation, "MAT.NET")
            Else
                If RadioButtonExclos.Checked Then
                    .Cod = DTOCliProductBlocked.Codis.Exclos
                    .Zip = ""
                ElseIf RadioButtonNA.Checked Then
                    .Cod = DTOCliProductBlocked.Codis.NoAplicable
                    .Zip = ""
                ElseIf RadioButtonExclusiva.Checked Then
                    .Cod = DTOCliProductBlocked.Codis.Exclusiva
                    .Zip = TextBoxZip.Text
                ElseIf RadioButtonDistribuidorOficial.Checked Then
                    .Cod = DTOCliProductBlocked.Codis.DistribuidorOficial
                    .Zip = ""
                ElseIf RadioButtonStandard.Checked Then
                    .Cod = DTOCliProductBlocked.Codis.Standard
                    .Zip = ""
                End If
                .Obs = TextBoxObs.Text

                Dim exs As New List(Of Exception)
                UIHelper.ToggleProggressBar(Panel1, True)
                If Await FEB.CliProductBlocked.Update(exs, _CliProductBlocked) Then
                    RaiseEvent AfterUpdate(_CliProductBlocked, MatEventArgs.Empty)
                    Me.Close()
                Else
                    UIHelper.ToggleProggressBar(Panel1, False)
                    UIHelper.WarnError(exs, "error al desar la fitxa")
                End If

            End If
        End With
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub


    Private Async Sub Xl_LookupProduct1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_LookupProduct1.AfterUpdate
        Dim exs As New List(Of Exception)
        Dim oProduct As DTOProduct = Xl_LookupProduct1.Product
        Dim oContact As DTOContact = _CliProductBlocked.Contact
        PanelDetall.Visible = True
        'PictureBoxLogo.Image = FEB.Product.Brand(exs,oProduct).Logo
        _CliProductBlocked = Await FEB.CliProductBlocked.Find(exs, oContact, oProduct)
        If exs.Count = 0 Then
            If _CliProductBlocked Is Nothing Then
                _CliProductBlocked = DTOCliProductBlocked.Factory(oContact, oProduct)
                Dim oBrand = FEB.Product.Brand(exs, oProduct)
                _CliProductBlocked.DistModel = IIf(oBrand.CodDist = DTOProductBrand.CodDists.DistribuidorsOficials, DTOCliProductBlocked.DistModels.Closed, DTOCliProductBlocked.DistModels.Open)
            End If
            EnableCheckboxes(oProduct)

            Await LoadClientsAmbExclusiva()
            TextBoxObs.Text = ""
        Else
            UIHelper.WarnError(exs)
        End If

    End Sub

    Private Sub EnableCheckboxes(oProduct As DTOProduct)
        If oProduct Is Nothing Then
            PanelDetall.Visible = False
        Else
            PanelDetall.Visible = True

            Dim IsRestrictedBrand As Boolean = _CliProductBlocked.DistModel = DTOCliProductBlocked.DistModels.Closed ' (febl.Product.BrandCodDist(oProduct) = DTOProductBrand.CodDists.DistribuidorsOficials)
            Dim IsFree As Boolean = Not IsRestrictedBrand

            PictureBoxStandard.Image = IIf(IsRestrictedBrand, My.Resources.wrong, My.Resources.Ok)
            TextBoxLabelStandard.Text = IIf(IsRestrictedBrand, "Opció per defecte. Comandes bloquejades", "Opció per defecte. Client habitual o potencial, sense restriccions en aquesta marca salvo que algú en tingui la exclusiva al seu codi postal")

            RadioButtonDistribuidorOficial.Visible = IsRestrictedBrand
            TextBox4.Visible = IsRestrictedBrand
            PictureBoxDistribuidorOficial.Visible = IsRestrictedBrand

            RadioButtonExclos.Enabled = IsFree
            TextBox1.Enabled = IsFree

        End If
    End Sub


End Class