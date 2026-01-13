Public Class Xl_Contact_Rep
    Inherits System.Windows.Forms.UserControl

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'UserControl overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents TextBoxAlias As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents DateTimePickerAlta As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents DateTimePickerBaja As System.Windows.Forms.DateTimePicker
    Friend WithEvents CheckBoxBaja As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBoxComisio As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TextBoxComStandard As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxComReducida As System.Windows.Forms.TextBox
    Friend WithEvents CheckBoxIVA As System.Windows.Forms.CheckBox
    Friend WithEvents ComboBoxIRPF As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TextBoxCustomIRPF As System.Windows.Forms.TextBox
    Friend WithEvents Xl_IBAN1 As Xl_Iban
    Friend WithEvents CheckBoxFiscal As System.Windows.Forms.CheckBox
    Friend WithEvents Xl_ContactFiscal As Xl_Contact2
    Friend WithEvents Xl_Image1 As Xl_Image
    Friend WithEvents Xl_RepProductsTree1 As Xl_RepProductsTree
    Friend WithEvents CheckBoxDisableLiqs As CheckBox
    Friend WithEvents TextBoxDescription As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents GroupBoxFacturacio As System.Windows.Forms.GroupBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.TextBoxAlias = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DateTimePickerAlta = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.DateTimePickerBaja = New System.Windows.Forms.DateTimePicker()
        Me.CheckBoxBaja = New System.Windows.Forms.CheckBox()
        Me.GroupBoxComisio = New System.Windows.Forms.GroupBox()
        Me.TextBoxComReducida = New System.Windows.Forms.TextBox()
        Me.TextBoxComStandard = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.CheckBoxIVA = New System.Windows.Forms.CheckBox()
        Me.ComboBoxIRPF = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TextBoxCustomIRPF = New System.Windows.Forms.TextBox()
        Me.CheckBoxFiscal = New System.Windows.Forms.CheckBox()
        Me.Xl_Image1 = New Mat.Net.Xl_Image()
        Me.Xl_IBAN1 = New Mat.Net.Xl_Iban()
        Me.Xl_ContactFiscal = New Mat.Net.Xl_Contact2()
        Me.GroupBoxFacturacio = New System.Windows.Forms.GroupBox()
        Me.Xl_RepProductsTree1 = New Mat.Net.Xl_RepProductsTree()
        Me.CheckBoxDisableLiqs = New System.Windows.Forms.CheckBox()
        Me.TextBoxDescription = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.GroupBoxComisio.SuspendLayout()
        Me.GroupBoxFacturacio.SuspendLayout()
        Me.SuspendLayout()
        '
        'TextBoxAlias
        '
        Me.TextBoxAlias.Location = New System.Drawing.Point(88, 0)
        Me.TextBoxAlias.Name = "TextBoxAlias"
        Me.TextBoxAlias.Size = New System.Drawing.Size(88, 20)
        Me.TextBoxAlias.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(24, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(32, 16)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Alias:"
        '
        'DateTimePickerAlta
        '
        Me.DateTimePickerAlta.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerAlta.Location = New System.Drawing.Point(88, 24)
        Me.DateTimePickerAlta.Name = "DateTimePickerAlta"
        Me.DateTimePickerAlta.Size = New System.Drawing.Size(88, 20)
        Me.DateTimePickerAlta.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(24, 30)
        Me.Label2.Margin = New System.Windows.Forms.Padding(3, 3, 3, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(32, 16)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Alta:"
        '
        'DateTimePickerBaja
        '
        Me.DateTimePickerBaja.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerBaja.Location = New System.Drawing.Point(88, 48)
        Me.DateTimePickerBaja.Name = "DateTimePickerBaja"
        Me.DateTimePickerBaja.Size = New System.Drawing.Size(88, 20)
        Me.DateTimePickerBaja.TabIndex = 4
        '
        'CheckBoxBaja
        '
        Me.CheckBoxBaja.Location = New System.Drawing.Point(8, 48)
        Me.CheckBoxBaja.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
        Me.CheckBoxBaja.Name = "CheckBoxBaja"
        Me.CheckBoxBaja.Size = New System.Drawing.Size(72, 16)
        Me.CheckBoxBaja.TabIndex = 5
        Me.CheckBoxBaja.Text = "Baja"
        '
        'GroupBoxComisio
        '
        Me.GroupBoxComisio.Controls.Add(Me.TextBoxComReducida)
        Me.GroupBoxComisio.Controls.Add(Me.TextBoxComStandard)
        Me.GroupBoxComisio.Controls.Add(Me.Label4)
        Me.GroupBoxComisio.Controls.Add(Me.Label3)
        Me.GroupBoxComisio.Location = New System.Drawing.Point(8, 105)
        Me.GroupBoxComisio.Name = "GroupBoxComisio"
        Me.GroupBoxComisio.Size = New System.Drawing.Size(168, 80)
        Me.GroupBoxComisio.TabIndex = 6
        Me.GroupBoxComisio.TabStop = False
        Me.GroupBoxComisio.Text = "Comisions:"
        '
        'TextBoxComReducida
        '
        Me.TextBoxComReducida.Location = New System.Drawing.Point(84, 41)
        Me.TextBoxComReducida.Name = "TextBoxComReducida"
        Me.TextBoxComReducida.Size = New System.Drawing.Size(32, 20)
        Me.TextBoxComReducida.TabIndex = 7
        '
        'TextBoxComStandard
        '
        Me.TextBoxComStandard.Location = New System.Drawing.Point(84, 19)
        Me.TextBoxComStandard.Name = "TextBoxComStandard"
        Me.TextBoxComStandard.Size = New System.Drawing.Size(32, 20)
        Me.TextBoxComStandard.TabIndex = 6
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(24, 41)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(56, 16)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "Reducida:"
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(24, 19)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(56, 16)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Standard:"
        '
        'CheckBoxIVA
        '
        Me.CheckBoxIVA.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxIVA.Location = New System.Drawing.Point(12, 48)
        Me.CheckBoxIVA.Name = "CheckBoxIVA"
        Me.CheckBoxIVA.Size = New System.Drawing.Size(54, 16)
        Me.CheckBoxIVA.TabIndex = 7
        Me.CheckBoxIVA.Text = "IVA"
        '
        'ComboBoxIRPF
        '
        Me.ComboBoxIRPF.FormattingEnabled = True
        Me.ComboBoxIRPF.Items.AddRange(New Object() {"exento", "standard", "custom"})
        Me.ComboBoxIRPF.Location = New System.Drawing.Point(51, 69)
        Me.ComboBoxIRPF.Name = "ComboBoxIRPF"
        Me.ComboBoxIRPF.Size = New System.Drawing.Size(72, 21)
        Me.ComboBoxIRPF.TabIndex = 8
        Me.ComboBoxIRPF.Text = "standard"
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(12, 72)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(32, 16)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "IRPF:"
        '
        'TextBoxCustomIRPF
        '
        Me.TextBoxCustomIRPF.Location = New System.Drawing.Point(129, 68)
        Me.TextBoxCustomIRPF.Name = "TextBoxCustomIRPF"
        Me.TextBoxCustomIRPF.Size = New System.Drawing.Size(32, 20)
        Me.TextBoxCustomIRPF.TabIndex = 10
        Me.TextBoxCustomIRPF.Text = "0"
        Me.TextBoxCustomIRPF.Visible = False
        '
        'CheckBoxFiscal
        '
        Me.CheckBoxFiscal.AutoSize = True
        Me.CheckBoxFiscal.Location = New System.Drawing.Point(8, 264)
        Me.CheckBoxFiscal.Name = "CheckBoxFiscal"
        Me.CheckBoxFiscal.Size = New System.Drawing.Size(159, 17)
        Me.CheckBoxFiscal.TabIndex = 17
        Me.CheckBoxFiscal.Text = "factures a un altre raó social"
        Me.CheckBoxFiscal.UseVisualStyleBackColor = True
        '
        'Xl_Image1
        '
        Me.Xl_Image1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Image1.Bitmap = Nothing
        Me.Xl_Image1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Xl_Image1.EmptyImageLabelText = "imatge 350x400 px"
        Me.Xl_Image1.IsDirty = False
        Me.Xl_Image1.Location = New System.Drawing.Point(296, 287)
        Me.Xl_Image1.Name = "Xl_Image1"
        Me.Xl_Image1.Size = New System.Drawing.Size(173, 199)
        Me.Xl_Image1.TabIndex = 19
        Me.Xl_Image1.ZipStream = Nothing
        '
        'Xl_IBAN1
        '
        Me.Xl_IBAN1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Xl_IBAN1.Location = New System.Drawing.Point(6, 94)
        Me.Xl_IBAN1.Name = "Xl_IBAN1"
        Me.Xl_IBAN1.Size = New System.Drawing.Size(250, 50)
        Me.Xl_IBAN1.TabIndex = 14
        '
        'Xl_ContactFiscal
        '
        Me.Xl_ContactFiscal.Contact = Nothing
        Me.Xl_ContactFiscal.Emp = Nothing
        Me.Xl_ContactFiscal.Location = New System.Drawing.Point(8, 287)
        Me.Xl_ContactFiscal.Name = "Xl_ContactFiscal"
        Me.Xl_ContactFiscal.ReadOnly = False
        Me.Xl_ContactFiscal.Size = New System.Drawing.Size(263, 20)
        Me.Xl_ContactFiscal.TabIndex = 18
        Me.Xl_ContactFiscal.Visible = False
        '
        'GroupBoxFacturacio
        '
        Me.GroupBoxFacturacio.Controls.Add(Me.Xl_IBAN1)
        Me.GroupBoxFacturacio.Controls.Add(Me.ComboBoxIRPF)
        Me.GroupBoxFacturacio.Controls.Add(Me.CheckBoxIVA)
        Me.GroupBoxFacturacio.Controls.Add(Me.Label5)
        Me.GroupBoxFacturacio.Controls.Add(Me.TextBoxCustomIRPF)
        Me.GroupBoxFacturacio.Location = New System.Drawing.Point(8, 313)
        Me.GroupBoxFacturacio.Name = "GroupBoxFacturacio"
        Me.GroupBoxFacturacio.Size = New System.Drawing.Size(263, 173)
        Me.GroupBoxFacturacio.TabIndex = 20
        Me.GroupBoxFacturacio.TabStop = False
        Me.GroupBoxFacturacio.Text = "facturació i pagos"
        '
        'Xl_RepProductsTree1
        '
        Me.Xl_RepProductsTree1.Location = New System.Drawing.Point(183, 0)
        Me.Xl_RepProductsTree1.Name = "Xl_RepProductsTree1"
        Me.Xl_RepProductsTree1.Size = New System.Drawing.Size(286, 218)
        Me.Xl_RepProductsTree1.TabIndex = 22
        '
        'CheckBoxDisableLiqs
        '
        Me.CheckBoxDisableLiqs.Location = New System.Drawing.Point(8, 72)
        Me.CheckBoxDisableLiqs.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
        Me.CheckBoxDisableLiqs.Name = "CheckBoxDisableLiqs"
        Me.CheckBoxDisableLiqs.Size = New System.Drawing.Size(169, 16)
        Me.CheckBoxDisableLiqs.TabIndex = 23
        Me.CheckBoxDisableLiqs.Text = "Desactiva noves liquidacions"
        '
        'TextBoxDescription
        '
        Me.TextBoxDescription.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxDescription.Location = New System.Drawing.Point(70, 232)
        Me.TextBoxDescription.MaxLength = 100
        Me.TextBoxDescription.Name = "TextBoxDescription"
        Me.TextBoxDescription.Size = New System.Drawing.Size(399, 20)
        Me.TextBoxDescription.TabIndex = 24
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(5, 235)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(59, 16)
        Me.Label6.TabIndex = 25
        Me.Label6.Text = "Descripcio:"
        '
        'Xl_Contact_Rep
        '
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.TextBoxDescription)
        Me.Controls.Add(Me.CheckBoxDisableLiqs)
        Me.Controls.Add(Me.Xl_RepProductsTree1)
        Me.Controls.Add(Me.GroupBoxFacturacio)
        Me.Controls.Add(Me.Xl_Image1)
        Me.Controls.Add(Me.Xl_ContactFiscal)
        Me.Controls.Add(Me.CheckBoxFiscal)
        Me.Controls.Add(Me.GroupBoxComisio)
        Me.Controls.Add(Me.CheckBoxBaja)
        Me.Controls.Add(Me.DateTimePickerBaja)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.DateTimePickerAlta)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBoxAlias)
        Me.Name = "Xl_Contact_Rep"
        Me.Size = New System.Drawing.Size(477, 506)
        Me.GroupBoxComisio.ResumeLayout(False)
        Me.GroupBoxComisio.PerformLayout()
        Me.GroupBoxFacturacio.ResumeLayout(False)
        Me.GroupBoxFacturacio.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Private _Rep As DTORep
    Private _AllowEvents As Boolean

    Public Shadows Async Function Load(oRep As DTORep) As Task
        Dim exs As New List(Of Exception)
        _Rep = oRep
        If FEB.Contact.Load(_Rep, exs) Then
            With _Rep
                TextBoxAlias.Text = .NickName
                If .FchAlta > DateTimePickerAlta.MinDate Then
                    DateTimePickerAlta.Value = .FchAlta
                Else
                    DateTimePickerAlta.Value = DTO.GlobalVariables.Today()
                End If

                If .FchBaja <= DateTimePickerBaja.MinDate Then
                    CheckBoxBaja.Checked = False
                    DateTimePickerBaja.Visible = False
                Else
                    CheckBoxBaja.Checked = True
                    DateTimePickerBaja.Visible = True
                    DateTimePickerBaja.Value = .FchBaja
                End If

                TextBoxDescription.Text = .Description

                CheckBoxDisableLiqs.Checked = .DisableLiqs

                TextBoxComStandard.Text = .ComisionStandard
                TextBoxComReducida.Text = .ComisionReducida
                CheckBoxIVA.Checked = (.IvaCod = DTORep.IVACods.standard)

                ComboBoxIRPF.SelectedIndex = CInt(.IrpfCod)
                TextBoxCustomIRPF.Visible = (.IrpfCod = DTOProveidor.IRPFCods.custom)
                TextBoxCustomIRPF.Text = .IrpfCustom


                If .RaoSocialFacturacio IsNot Nothing Then
                    CheckBoxFiscal.Checked = True
                    Xl_ContactFiscal.Contact = .RaoSocialFacturacio
                End If

                Xl_ContactFiscal.Visible = CheckBoxFiscal.Checked
                GroupBoxFacturacio.Visible = Not CheckBoxFiscal.Checked

                Dim oIban = Await FEB.Iban.FromContact(exs, _Rep, DTOIban.Cods.Proveidor)
                If exs.Count = 0 Then
                    Await Xl_IBAN1.Load(oIban, _Rep, DTOIban.Cods.proveidor)
                Else
                    UIHelper.WarnError(exs)
                End If

                Xl_Image1.Bitmap = LegacyHelper.ImageHelper.FromBytes(Await FEB.Rep.Foto(exs, _Rep))
            End With

            Await refrescaRepProducts()
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Public ReadOnly Property Rep As DTORep
        Get
            With _Rep
                .NickName = TextBoxAlias.Text
                .FchAlta = DateTimePickerAlta.Value
                If CheckBoxBaja.Checked Then
                    .FchBaja = DateTimePickerBaja.Value
                Else
                    .FchBaja = Nothing
                End If
                .DisableLiqs = CheckBoxDisableLiqs.Checked

                If IsNumeric(TextBoxComStandard.Text) Then
                    .ComisionStandard = TextBoxComStandard.Text
                End If

                If IsNumeric(TextBoxComReducida.Text) Then
                    .ComisionReducida = TextBoxComReducida.Text
                End If
                .Description = TextBoxDescription.Text
                .IvaCod = IIf(CheckBoxIVA.Checked, DTORep.IVACods.standard, DTORep.IVACods.exento)
                .IrpfCod = ComboBoxIRPF.SelectedIndex
                .IrpfCustom = TextBoxCustomIRPF.Text
                If CheckBoxFiscal.Checked Then
                    .RaoSocialFacturacio = DTOProveidor.FromContact(Xl_ContactFiscal.Contact)
                Else
                    .RaoSocialFacturacio = Nothing
                End If
                If Xl_Image1.Bitmap Is Nothing Then
                    .Foto = Nothing
                Else
                    Using ms As New IO.MemoryStream
                        Xl_Image1.Bitmap.Save(ms, Imaging.ImageFormat.Jpeg)
                        .Foto = ms.ToArray()
                    End Using
                End If
            End With
            Return _Rep
        End Get
    End Property


    Private Sub DirtyControl(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
            TextBoxAlias.TextChanged,
        DateTimePickerAlta.ValueChanged,
        DateTimePickerBaja.ValueChanged,
        CheckBoxDisableLiqs.CheckedChanged,
        TextBoxComReducida.TextChanged,
        TextBoxComStandard.TextChanged,
        CheckBoxIVA.CheckedChanged,
        ComboBoxIRPF.SelectedIndexChanged,
        TextBoxCustomIRPF.TextChanged

        If _AllowEvents Then
            RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
        End If
    End Sub



    Private Sub CheckBoxBaja_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxBaja.CheckedChanged
        DateTimePickerBaja.Visible = CheckBoxBaja.Checked
        If _AllowEvents Then
            RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
        End If
    End Sub

    Private Async Sub Xl_RepProductsTree1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_RepProductsTree1.RequestToRefresh
        Await refrescaRepProducts()
    End Sub

    Private Async Sub refrescaRepProducts(sender As Object, e As MatEventArgs)
        RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
        Await refrescaRepProducts()
    End Sub

    Private Async Function refrescaRepProducts() As Task
        Dim exs As New List(Of Exception)
        Dim oRepProducts = Await FEB.RepProducts.All(exs, Current.Session.Emp, _Rep, True)
        If exs.Count = 0 Then
            Xl_RepProductsTree1.Load(oRepProducts)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function



    Private Sub CheckBoxFiscal_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxFiscal.CheckedChanged
        If _AllowEvents Then
            Xl_ContactFiscal.Visible = CheckBoxFiscal.Checked
            GroupBoxFacturacio.Visible = Not CheckBoxFiscal.Checked
            RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
        End If
    End Sub

    Private Sub Xl_ContactFiscal_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_ContactFiscal.AfterUpdate
        If _AllowEvents Then
            RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
        End If
    End Sub

    Private Sub Xl_Image1_AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs) Handles Xl_Image1.AfterUpdate
        If _AllowEvents Then
            RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
        End If
    End Sub



    Private Sub Xl_IBAN1_RequestToChange(sender As Object, e As MatEventArgs) Handles Xl_IBAN1.RequestToChange, Xl_IBAN1.RequestToAddNew
        Dim oContact As DTOContact = New DTOContact(_Rep.Guid)
        Dim oFrm As New Frm_Contact_Ibans(oContact)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshIban
        oFrm.Show()
    End Sub

    Private Async Sub RefreshIban(sender As Object, e As MatEventArgs)
        Dim exs As New List(Of Exception)
        Dim oContact As DTOContact = New DTOContact(_Rep.Guid)
        Dim oIban = Await FEB.Iban.FromContact(exs, oContact, DTOIban.Cods.Proveidor)
        If exs.Count = 0 Then
            Await Xl_IBAN1.Load(oIban)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Xl_RepProducts1_RequestToAddNew(sender As Object, e As MatEventArgs)
        Dim exs As New List(Of Exception)
        Dim oRep = Await FEB.Rep.Find(_Rep.Guid, exs)
        If exs.Count = 0 Then
            Dim oRepProduct As New DTORepProduct()
            With oRepProduct
                .Rep = oRep
                .ComRed = _Rep.ComisionReducida
                .ComStd = _Rep.ComisionStandard
                .FchFrom = DTO.GlobalVariables.Today()
                .Cod = DTORepProduct.Cods.Included
            End With
            Dim values As New List(Of DTORepProduct)
            values.Add(oRepProduct)
            Dim oFrm As New Frm_RepProduct(values)
            AddHandler oFrm.AfterUpdate, AddressOf refrescaRepProducts
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Xl_ContactFiscal_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_ContactFiscal.AfterUpdate

    End Sub

    Private Sub Xl_RepProductsTree1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_RepProductsTree1.RequestToAddNew
        Dim item As New DTORepProduct
        With item
            .Rep = _Rep
            .DistributionChannel = Xl_RepProductsTree1.DistributionChannel
            .Area = Xl_RepProductsTree1.Area
            .Product = Xl_RepProductsTree1.Product
            .FchFrom = DTO.GlobalVariables.Today()
            .Cod = DTORepProduct.Cods.Included
            .ComStd = _Rep.ComisionStandard
            .ComRed = _Rep.ComisionReducida
        End With

        Dim items As New List(Of DTORepProduct)
        items.Add(item)

        Dim oFrm As New Frm_RepProduct(items)
        AddHandler oFrm.AfterUpdate, AddressOf refrescaRepProducts
        oFrm.Show()
    End Sub


End Class
