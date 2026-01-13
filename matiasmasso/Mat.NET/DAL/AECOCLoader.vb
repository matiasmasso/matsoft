Public Class AECOCLoader


    Shared Function NextEanToContact(ByVal oContact As DTOContact, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        Dim oEan As DTOEan = GetNextEan(oContact.Emp)

        If Update(oEan, oContact, exs) Then
            oContact.GLN = oEan
            retval = True
        End If

        Return retval
    End Function

    Shared Function NextEanToSku(oEmp As DTOEmp, ByRef oSku As DTOProductSku, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        Dim oEan As DTOEan = GetNextEan(oEmp)

        If Update(oEan, oSku, exs) Then
            oSku.Ean13 = oEan
            retval = True
        End If

        Return retval
    End Function

    Shared Function GetNextEan(oEmp As DTOEmp) As DTOEan
        Dim retval As DTOEan = Nothing
        Dim sNextEan As String = "0000000000000"
        Dim sRoot As String = EanRoot(oEmp)
        Dim iRootLen As Integer = sRoot.Length
        Dim oLastEan As DTOEan = LastEan()
        If Not oLastEan Is Nothing Then
            Dim sLastEan As String = oLastEan.RemoveControlDigit
            Dim sRootLess As String = sLastEan.Substring(iRootLen)
            Dim iIdx As Integer = CInt(sRootLess)
            Dim iNextIdx As Integer = iIdx + 1
            Dim sNextId As String = iNextIdx.ToString.PadLeft(12 - iRootLen, "0")
            Dim sControlDigit As Integer = ControlDigit(sRoot & sNextId)
            sNextEan = String.Format("{0}{1}{2}", sRoot, sNextId, sControlDigit)
            retval = DTOEan.Factory(sNextEan)
        End If
        Return retval
    End Function

    Shared Function EanRoot(oEmp As DTOEmp) As String
        Dim retval As String = ""
        Dim oDefaultValue As DTODefault = DefaultLoader.Find(DTODefault.Codis.AECOCroot, oEmp)
        If oDefaultValue IsNot Nothing Then
            retval = DefaultLoader.Find(DTODefault.Codis.AECOCroot, oEmp).Value
        End If
        Return retval
    End Function

    Shared Function ControlDigit(s As String) As String

        'split 12 digit source string into digits
        s = Left(s, 12)
        Dim digits As New List(Of Integer)
        For i As Integer = 0 To s.Length - 1
            digits.Add(s.Substring(i, 1))
        Next

        'Starting with the first number on the right, add all the alternate numbers 
        'Starting with the second number on the right, add all alternate numbers.
        Dim AlternateSum(2) As Integer
        For i As Integer = digits.Count - 1 To 0 Step -2
            AlternateSum(0) += digits(i)
            AlternateSum(1) += digits(i - 1)
        Next

        'Multiply the first result by three and add the second result
        Dim AlternateResult = AlternateSum(0) * 3 + AlternateSum(1)

        'The Number which you need to add to make it the next multiple of 10 Is the check digit. 
        Dim retval As Integer = 10 - (AlternateResult Mod 10)
        If retval = 10 Then retval = 0
        Return retval.ToString
    End Function

    Shared Function LastEan() As DTOEan
        Dim retval As DTOEan = Nothing
        Dim SQL As String = "SELECT MAX(EAN) AS LASTEAN FROM AECOC"
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = DTOEan.Factory(oDrd("LASTEAN").ToString())
        End If
        oDrd.Close()
        Return retval
    End Function



    Shared Function Update(oEan As DTOEan, oBaseGuid As DTOBaseGuid, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            UpdateAECOC(oEan, oBaseGuid, oTrans)

            If TypeOf oBaseGuid Is DTOContact Then
                UpdateContact(oEan, oBaseGuid, oTrans)
            ElseIf TypeOf oBaseGuid Is DTOProductSku Then
                UpdateSku(oEan, oBaseGuid, oTrans)
            Else
                Throw New Exception("tipus no contemplat. Nomes es per contactes i productes")
            End If

            oTrans.Commit()
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function

    Shared Sub UpdateAECOC(ByVal oEan As DTOEan, ByVal oBaseGuid As DTOBaseGuid, oTrans As SqlTransaction)
        Dim retval As Boolean = False
        Dim SQL As String = String.Format("INSERT INTO AECOC (Ean,Guid) VALUES ('{0}','{1}')", oEan.Value, oBaseGuid.Guid.ToString())
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub UpdateSku(ByVal oEan As DTOEan, ByVal oSku As DTOProductSku, oTrans As SqlTransaction)
        Dim retval As Boolean = False
        Dim SQL As String = String.Format("UPDATE Art SET CBar='{0}' WHERE Guid='{1}'", oEan.Value, oSku.Guid.ToString())
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub UpdateContact(ByVal oEan As DTOEan, ByVal oContact As DTOContact, oTrans As SqlTransaction)
        Dim retval As Boolean = False
        Dim SQL As String = String.Format("UPDATE CliGral SET GLN='{0}' WHERE Guid='{1}'", oEan.Value, oContact.Guid.ToString())
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub
End Class
