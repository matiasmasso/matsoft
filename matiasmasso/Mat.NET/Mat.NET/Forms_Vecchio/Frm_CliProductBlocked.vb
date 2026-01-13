Public Class Frm_CliProductBlocked

    Private _CliProductBlocked As DTO.DTOCliProductBlocked
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oCliProductBlocked As DTO.DTOCliProductBlocked)
        MyBase.New()
        Me.InitializeComponent()
        _CliProductBlocked = oCliProductBlocked
        BLL.BLLCliProductBlocked.Load(_CliProductBlocked)
        Refresca()
        _AllowEvents = True
    End Sub

    Private Sub Refresca()

        With _CliProductBlocked
            TextBoxCliNom.Text = .Contact.FullNom
            If .Product Is Nothing Then
                PanelDetall.Visible = False
                'Xl_LookupProduct1.Load(BLL_App.Catalog, Nothing)
            Else
                PanelDetall.Visible = True

                'PictureBoxLogo.Image = BLL_Product.Brand.Product.Tpa.Image
                Xl_LookupProduct1.Product = .Product
                LoadClientsAmbExclusiva()
                TextBoxObs.Text = .Obs

                Dim oProduct As DTOProduct = Xl_LookupProduct1.Product
                EnableCheckboxes(oProduct)

                Select Case .Cod
                    Case DTO.DTOCliProductBlocked.Codis.Standard
                        RadioButtonStandard.Checked = True
                    Case DTO.DTOCliProductBlocked.Codis.DistribuidorOficial
                        RadioButtonDistribuidorOficial.Checked = True
                    Case DTO.DTOCliProductBlocked.Codis.Exclusiva
                        RadioButtonExclusiva.Checked = True
                        TextBoxZip.Text = _CliProductBlocked.Zip
                    Case DTO.DTOCliProductBlocked.Codis.Exclos
                        RadioButtonExclos.Checked = True
                    Case DTO.DTOCliProductBlocked.Codis.NoAplicable
                        RadioButtonNA.Checked = True
                    Case DTO.DTOCliProductBlocked.Codis.AltresEnExclusiva
                        RadioButtonAltresExclusiva.Checked = True
                End Select
            End If
        End With

        EnableControls()
    End Sub

    Private Sub LoadClientsAmbExclusiva()
        Dim oContacts As List(Of DTOContact) = BLL.BLLCliProductBlocked.AltresEnExclusiva(_CliProductBlocked.Contact, _CliProductBlocked.Product)
        With ListBoxClis
            .DisplayMember = "Nom"
            .ValueMember = "Guid"
            .DataSource = oContacts
        End With
    End Sub

    Private Sub EnableControls()
        Dim BlEnableWarn As Boolean = ListBoxClis.Items.Count > 0

        If _CliProductBlocked.Product IsNot Nothing Then

            If BLL.BLLProduct.BrandCodDist(_CliProductBlocked.Product) = DTOProductBrand.CodDists.Free Then
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

    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadioButtonExclos.CheckedChanged, RadioButtonNA.CheckedChanged, RadioButtonExclusiva.CheckedChanged, RadioButtonDistribuidorOficial.CheckedChanged, TextBoxZip.TextChanged, RadioButtonAltresExclusiva.CheckedChanged, RadioButtonStandard.CheckedChanged
        If _AllowEvents Then
            ButtonOk.Enabled = True
            EnableControls()
        End If
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With _CliProductBlocked
            .Product = Xl_LookupProduct1.Product
            If .Product Is Nothing Then
                MsgBox("cal triar una marca o producte", MsgBoxStyle.Exclamation, "MAT.NET")
            Else
                If RadioButtonExclos.Checked Then
                    .Cod = DTO.DTOCliProductBlocked.Codis.Exclos
                    .Zip = ""
                ElseIf RadioButtonNA.Checked Then
                    .Cod = DTO.DTOCliProductBlocked.Codis.NoAplicable
                    .Zip = ""
                ElseIf RadioButtonExclusiva.Checked Then
                    .Cod = DTO.DTOCliProductBlocked.Codis.Exclusiva
                    .Zip = TextBoxZip.Text
                ElseIf RadioButtonDistribuidorOficial.Checked Then
                    .Cod = DTO.DTOCliProductBlocked.Codis.DistribuidorOficial
                    .Zip = ""
                ElseIf RadioButtonStandard.Checked Then
                    .Cod = DTO.DTOCliProductBlocked.Codis.Standard
                    .Zip = ""
                End If
                .Obs = TextBoxObs.Text

                Dim exs As New List(Of exception)
                If BLL.BLLCliProductBlocked.Update(_CliProductBlocked, exs) Then
                    RaiseEvent AfterUpdate(_CliProductBlocked, MatEventArgs.Empty)
                    Me.Close()
                Else
                    UIHelper.WarnError(exs, "error al desar la fitxa")
                End If

            End If
        End With
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub


    Private Sub Xl_LookupProduct1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_LookupProduct1.AfterUpdate
        Dim oProduct As DTOProduct = Xl_LookupProduct1.Product
        PanelDetall.Visible = True
        'PictureBoxLogo.Image = BLL.BLLProduct.Brand(oProduct).Logo
        _CliProductBlocked.Product = oProduct

        EnableCheckboxes(oProduct)

        LoadClientsAmbExclusiva()
        TextBoxObs.Text = ""

    End Sub

    Private Sub EnableCheckboxes(oProduct As DTOProduct)
        If oProduct Is Nothing Then
            PanelDetall.Visible = False
        Else
            PanelDetall.Visible = True

            Dim IsRestrictedBrand As Boolean = (BLL.BLLProduct.BrandCodDist(oProduct) = DTOProductBrand.CodDists.DistribuidorsOficials)
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