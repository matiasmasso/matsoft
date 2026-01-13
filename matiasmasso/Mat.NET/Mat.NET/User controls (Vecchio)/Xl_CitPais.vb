

Public Class Xl_CitPais
    Private mZip As Zip

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Property Zip() As Zip
        Get
            Return mZip
        End Get
        Set(ByVal value As Zip)
            mZip = value
            Refresca()
        End Set
    End Property

    Private Sub Refresca()
        SetSizes()
        If mZip Is Nothing Then
            TextBox1.Clear()
        Else
            TextBox1.Text = mZip.ZipyCityZon
            SetFlag()
        End If
    End Sub

    Private Sub SetFlag()
        If Not HideFlag() Then
            PictureBox1.Image = mZip.Country.Flag
        End If
    End Sub

    Private Function HideFlag() As Boolean
        Try
            HideFlag = mZip.Country.ISO = "ES"
        Catch ex As Exception
            HideFlag = True
        End Try
    End Function

    Private Sub SetSizes()
        If HideFlag() Then
            PictureBox1.Visible = False
            TextBox1.Width = Me.Width
        Else
            PictureBox1.Visible = True
            Dim i As Integer = Me.Width - PictureBox1.Width
            TextBox1.Width = i
            PictureBox1.Left = i
        End If
    End Sub

    Private Sub TextBox1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.DoubleClick
        'Dim oFrm As New Frm_Zip_Old(mZip)
        'AddHandler oFrm.AfterUpdate, AddressOf AfterZipUpdate
        'oFrm.Show()
    End Sub


    Private Sub TextBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyDown
        Select Case e.KeyCode
            Case Keys.Tab
                GetCit()
        End Select
    End Sub

    Private Sub TextBox1_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TextBox1.Validating
        GetCit()
        If mZip Is Nothing And TextBox1.Text > "" Then
            e.Cancel = True
        End If
    End Sub

    Private Sub GetCit()
        If mZip Is Nothing Then
            If TextBox1.Text = "" Then Exit Sub
        Else
            If TextBox1.Text = mZip.ZipyCityZon Then Exit Sub
        End If

        If TextBox1.Text = "" Then
            mZip = Nothing
            SetSizes()
        Else
            Dim oZips As MaxiSrvr.Zips = Zips.Search(TextBox1.Text)
            Select Case oZips.Count
                Case 0
                    Dim rc As MsgBoxResult
                    rc = MsgBox("Població no registrada." & vbCrLf & "La donem d'alta?", MsgBoxStyle.OkCancel, "M+O")
                    If rc = MsgBoxResult.Ok Then

                        Dim oDefaultArea As area = Nothing
                        If IsNumeric(TextBox1.Text) Then
                            Dim oDefaultZona As Zona = Zona.GuessFromZipCode(TextBox1.Text)
                            If oDefaultZona IsNot Nothing Then oDefaultArea = New area(oDefaultZona)
                        End If

                        Dim oFrm As New Frm_Geo(BLL.BLLApp.Org.Address.Zip.Location, Frm_Geo.SelectModes.SelectZip)
                        AddHandler oFrm.onItemSelected, AddressOf AddNewCit_AfterZonSelect
                        oFrm.Show()
                        Exit Sub
                        'mCit = root.WzNewCit(TextBox1.Text)
                    Else
                        mZip = Nothing
                        SetSizes()
                        Exit Sub
                    End If
                Case 1
                    mZip = oZips(0)
                Case Else
                    mZip = SelectZipFromZips(oZips)
            End Select
            Refresca()
        End If

        RaiseEvent AfterUpdate(Me, New System.EventArgs)
    End Sub


    Private Sub Xl_CitPais_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        SetSizes()
    End Sub

    Private Sub AddNewCit_AfterZonSelect(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oArea As area = CType(sender, area)
        If oArea.ValueType = area.ValueTypes.Zona Then

            Dim sSrc As String = TextBox1.Text
            Dim sCit As String = ""
            Dim sZip As String = ""
            If IsNumeric(sSrc.Substring(0, 1)) Then
                If sSrc.Contains(" ") Then
                    Dim sBlankPos As Integer = sSrc.IndexOf(" ")
                    sZip = sSrc.Substring(0, sBlankPos)
                    sCit = sSrc.Substring(sBlankPos + 1)
                Else
                    sZip = sSrc
                End If
            Else
                sCit = sSrc
            End If

            Dim oZona As Zona = oArea.Value
            Dim oLocation As Location = MaxiSrvr.Location.Find(sCit, oZona)
            If oLocation Is Nothing Then
                oLocation = New Location(oZona)
                oLocation.Nom = sCit
            End If
            Dim oZip As New Zip(oLocation)
            oZip.ZipCod = sZip

            'Dim oFrm As New Frm_Zip_Old(oZip)
            'AddHandler oFrm.AfterUpdate, AddressOf AfterZipUpdate
            'oFrm.Show()
        End If
    End Sub

    Private Sub AfterZipUpdate(ByVal sender As Object, ByVal e As System.EventArgs)
        mZip = CType(sender, Zip)
        Refresca()
        RaiseEvent AfterUpdate(Me, New System.EventArgs)
    End Sub
End Class
