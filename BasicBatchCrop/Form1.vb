Imports System.Drawing
Imports System.Drawing.Imaging
Imports System
Imports System.IO
Imports System.Collections

Public Class Form1
    'Barely any comments sorry, this was based on https://www.codeproject.com/Articles/12789/Merging-Images-in-NET
    'Actually BasicWaterMark is what this evolved from (based on above) and this is now also based on: https://stackoverflow.com/a/8726255
    Dim img As Image
    Dim strWatermark As String
    Dim strFolder As String
    Dim strOut As String
    Dim x As Long
    Dim y As Long
    Dim x2 As Long
    Dim y2 As Long


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'Select input folder
        FolderBrowserDialog1.ShowDialog()
        If (FolderBrowserDialog1.SelectedPath IsNot "") Then
            strFolder = FolderBrowserDialog1.SelectedPath
        End If

        Button4.Enabled = True
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        'GO!
        If (strFolder IsNot "" And strOut IsNot "") Then
            Dim inputImgs As String() = Directory.GetFiles(strFolder)

            Dim filename As String
            For Each filename In inputImgs
                Try
                    Dim inImg As Image
                    inImg = Image.FromFile(filename)
                    'Dim OrigRect As New Rectangle(0, 0, inImg.Width, inImg.Height)

                    Dim actX As Long
                    Dim actY As Long

                    Dim actX2 As Long
                    Dim actY2 As Long

                    actX = x
                    actY = y

                    actX2 = x2
                    actY2 = y2

                    If (CheckBox1.Checked) Then
                        'Actually we do a percentage
                        actX = Math.Round(inImg.Width * (x / 100))
                        actY = Math.Round(inImg.Height * (y / 100))
                    End If

                    If (CheckBox2.Checked) Then
                        'Actually we do a percentage
                        actX2 = Math.Round(inImg.Width * (x2 / 100))
                        actY2 = Math.Round(inImg.Height * (y2 / 100))
                    End If

                    Dim CropRect As New Rectangle(actX, actY, actX2 - actX, actY2 - actY)
                    Dim CropImage = New Bitmap(CropRect.Width, CropRect.Height)

                    Using grp = Graphics.FromImage(CropImage)
                        grp.DrawImage(inImg, New Rectangle(0, 0, CropRect.Width, CropRect.Height), CropRect, GraphicsUnit.Pixel)
                    End Using

                    Dim newOut As String

                    newOut = strOut + "\" + Path.GetFileName(filename)

                    If (filename.ToUpper.Contains("JPG") Or filename.ToUpper.Contains("JPEG")) Then
                        'JPEG format
                        CropImage.Save(newOut, ImageFormat.Jpeg)
                    End If

                    If (filename.ToUpper.Contains("PNG")) Then
                        'PNG format
                        CropImage.Save(newOut, ImageFormat.Png)
                    End If

                    If (filename.ToUpper.Contains("GIF")) Then
                        'GIF format
                        CropImage.Save(newOut, ImageFormat.Gif)
                    End If

                    inImg.Dispose()
                    CropImage.Dispose()
                Catch ex As Exception

                End Try

            Next

            MessageBox.Show("DONE! :D")
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        'select our output folder
        FolderBrowserDialog1.ShowDialog()

        If (FolderBrowserDialog1.SelectedPath IsNot "") Then
            strOut = FolderBrowserDialog1.SelectedPath
        End If

        Button3.Enabled = True
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        'X
        Try
            x = TextBox1.Text
        Catch ex As Exception
            TextBox1.Text = "0"
        End Try
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        'Y
        Try
            y = TextBox2.Text
        Catch ex As Exception
            TextBox2.Text = "0"
        End Try

    End Sub

    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged
        'X2
        Try
            x2 = TextBox4.Text
        Catch ex As Exception
            TextBox4.Text = "0"
        End Try
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        'Y2
        Try
            y2 = TextBox3.Text
        Catch ex As Exception
            TextBox3.Text = "0"
        End Try
    End Sub

End Class
