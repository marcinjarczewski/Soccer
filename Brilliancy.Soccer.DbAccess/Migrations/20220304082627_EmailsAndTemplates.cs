﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Brilliancy.Soccer.DbAccess.Migrations
{
    public partial class EmailsAndTemplates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Emails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Recipient = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateSent = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Counter = table.Column<int>(type: "int", nullable: false),
                    LastErrorMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastErrorDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Templates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TranslateContentId = table.Column<int>(type: "int", nullable: true),
                    Header = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TranslateHeaderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Templates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Templates_Translations_TranslateContentId",
                        column: x => x.TranslateContentId,
                        principalTable: "Translations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Templates_Translations_TranslateHeaderId",
                        column: x => x.TranslateHeaderId,
                        principalTable: "Translations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Polish" },
                    { 2, "English" }
                });

            migrationBuilder.InsertData(
                table: "Translations",
                column: "Id",
                values: new object[]
                {
                    1,
                    2
                });

            migrationBuilder.InsertData(
                table: "Templates",
                columns: new[] { "Id", "Content", "Header", "TranslateContentId", "TranslateHeaderId" },
                values: new object[] { 1, "<!doctype html>\r\n<html>\r\n<head>\r\n    <meta name=\"viewport\" content=\"width=device-width\" />\r\n    <meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\" />\r\n    <title>@Model.Subject</title>\r\n    <style>\r\n        /* -------------------------------------\r\n            GLOBAL RESETS\r\n        ------------------------------------- */\r\n        img {\r\n            border: none;\r\n            -ms-interpolation-mode: bicubic;\r\n            max-width: 100%;\r\n        }\r\n\r\n        body {\r\n            font-family: sans-serif;\r\n            -webkit-font-smoothing: antialiased;\r\n            font-size: 14px;\r\n            line-height: 1.4;\r\n            margin: 0;\r\n            padding: 0;\r\n            -ms-text-size-adjust: 100%;\r\n            -webkit-text-size-adjust: 100%;\r\n            height: 100%;\r\n            width: 100%;\r\n        }\r\n\r\n        table {\r\n            border-collapse: separate;\r\n            mso-table-lspace: 0pt;\r\n            mso-table-rspace: 0pt;\r\n            width: 100%;\r\n        }\r\n\r\n            table td {\r\n                font-family: sans-serif;\r\n                font-size: 14px;\r\n                vertical-align: top;\r\n            }\r\n        /* -------------------------------------\r\n            BODY & CONTAINER\r\n        ------------------------------------- */\r\n        html {\r\n            height: 100%;\r\n        }\r\n\r\n        .body {\r\n            position: relative;\r\n            z-index: 9999;\r\n        }\r\n\r\n        /* Set a max-width, and make it display as block so it will automatically stretch to that width, but will also shrink down on a phone or something */\r\n        .container {\r\n            display: block;\r\n            Margin: 0 auto !important;\r\n            /* makes it centered */\r\n            max-width: 580px;\r\n            padding: 30px 10px;\r\n            width: 580px;\r\n        }\r\n        /* This should also be a block element, so that it will fill 100% of the .container */\r\n        .content {\r\n            box-sizing: border-box;\r\n            display: block;\r\n            Margin: 0 auto;\r\n            max-width: 580px;\r\n            padding: 10px;\r\n        }\r\n        /* -------------------------------------\r\n            HEADER, FOOTER, MAIN\r\n        ------------------------------------- */\r\n        .main {\r\n            background: #fff;\r\n            border-radius: 3px;\r\n            width: 100%;\r\n        }\r\n\r\n        .wrapper {\r\n            box-sizing: border-box;\r\n            padding: 20px;\r\n        }\r\n\r\n        .footer {\r\n            clear: both;\r\n            padding-top: 10px;\r\n            text-align: center;\r\n            width: 100%;\r\n        }\r\n\r\n            .footer td,\r\n            .footer p,\r\n            .footer span,\r\n            .footer a {\r\n                color: #fff;\r\n                font-size: 12px;\r\n                text-align: center;\r\n            }\r\n        /* -------------------------------------\r\n            TYPOGRAPHY\r\n        ------------------------------------- */\r\n        h1,\r\n        h2,\r\n        h3,\r\n        h4 {\r\n            color: #000000;\r\n            font-family: sans-serif;\r\n            font-weight: 400;\r\n            line-height: 1.4;\r\n            margin: 0;\r\n            Margin-bottom: 20px;\r\n        }\r\n\r\n        h1 {\r\n            font-size: 35px;\r\n            font-weight: 300;\r\n            text-align: center;\r\n            text-transform: capitalize;\r\n        }\r\n\r\n        p,\r\n        ul,\r\n        ol {\r\n            font-family: sans-serif;\r\n            font-size: 14px;\r\n            font-weight: normal;\r\n            margin: 0;\r\n            Margin-bottom: 15px;\r\n        }\r\n\r\n            p li,\r\n            ul li,\r\n            ol li {\r\n                list-style-position: inside;\r\n                margin-left: 5px;\r\n            }\r\n\r\n        a {\r\n            color: #3498db;\r\n            text-decoration: underline;\r\n        }\r\n        /* -------------------------------------\r\n            BUTTONS\r\n        ------------------------------------- */\r\n        .btn {\r\n            box-sizing: border-box;\r\n            width: 100%;\r\n        }\r\n\r\n            .btn > tbody > tr > td {\r\n                padding-bottom: 15px;\r\n            }\r\n\r\n            .btn table {\r\n                width: auto;\r\n            }\r\n\r\n                .btn table td {\r\n                    background-color: #ffffff;\r\n                    border-radius: 5px;\r\n                    text-align: center;\r\n                }\r\n\r\n            .btn a {\r\n                background-color: #ffffff;\r\n                border: solid 1px #3498db;\r\n                box-sizing: border-box;\r\n                color: #3498db;\r\n                cursor: pointer;\r\n                display: inline-block;\r\n                font-size: 16px;\r\n                font-weight: bold;\r\n                margin: 0;\r\n                padding: 12px 25px;\r\n                text-decoration: none;\r\n                text-transform: capitalize;\r\n            }\r\n\r\n        .btn-primary a {\r\n            border: 3px solid #00c851;\r\n            color: #00c851;\r\n        }\r\n        /* -------------------------------------\r\n            OTHER STYLES THAT MIGHT BE USEFUL\r\n        ------------------------------------- */\r\n        .last {\r\n            margin-bottom: 0;\r\n        }\r\n\r\n        .first {\r\n            margin-top: 0;\r\n        }\r\n\r\n        .align-center {\r\n            text-align: center;\r\n        }\r\n\r\n        .align-right {\r\n            text-align: right;\r\n        }\r\n\r\n        .align-left {\r\n            text-align: left;\r\n        }\r\n\r\n        .clear {\r\n            clear: both;\r\n        }\r\n\r\n        .mt0 {\r\n            margin-top: 0;\r\n        }\r\n\r\n        .mb0 {\r\n            margin-bottom: 0;\r\n        }\r\n\r\n        .preheader {\r\n            color: transparent;\r\n            display: none;\r\n            height: 0;\r\n            max-height: 0;\r\n            max-width: 0;\r\n            opacity: 0;\r\n            overflow: hidden;\r\n            mso-hide: all;\r\n            visibility: hidden;\r\n            width: 0;\r\n        }\r\n\r\n        .powered-by a {\r\n            text-decoration: none;\r\n        }\r\n\r\n        hr {\r\n            border: 0;\r\n            border-bottom: 1px solid #f6f6f6;\r\n            Margin: 20px 0;\r\n        }\r\n        /* -------------------------------------\r\n            RESPONSIVE AND MOBILE FRIENDLY STYLES\r\n        ------------------------------------- */\r\n        @media only screen and (max-width: 620px) {\r\n            table[class=body] h1 {\r\n                font-size: 28px !important;\r\n                margin-bottom: 10px !important;\r\n            }\r\n\r\n            table[class=body] p,\r\n            table[class=body] ul,\r\n            table[class=body] ol,\r\n            table[class=body] td,\r\n            table[class=body] span,\r\n            table[class=body] a {\r\n                font-size: 16px !important;\r\n            }\r\n\r\n            table[class=body] .wrapper,\r\n            table[class=body] .article {\r\n                padding: 10px !important;\r\n            }\r\n\r\n            table[class=body] .content {\r\n                padding: 0 !important;\r\n            }\r\n\r\n            table[class=body] .container {\r\n                padding: 0 !important;\r\n                width: 100% !important;\r\n            }\r\n\r\n            table[class=body] .main {\r\n                border-left-width: 0 !important;\r\n                border-radius: 0 !important;\r\n                border-right-width: 0 !important;\r\n            }\r\n\r\n            table[class=body] .btn table {\r\n                width: 100% !important;\r\n            }\r\n\r\n            table[class=body] .btn a {\r\n                width: 100% !important;\r\n            }\r\n\r\n            table[class=body] .img-responsive {\r\n                height: auto !important;\r\n                max-width: 100% !important;\r\n                width: auto !important;\r\n            }\r\n\r\n            table[class=body] .img-responsive {\r\n                height: auto !important;\r\n                max-width: 100% !important;\r\n                width: auto !important;\r\n            }\r\n\r\n            table[class=body] img {\r\n                text-align: center;\r\n            }\r\n        }\r\n        /* -------------------------------------\r\n            PRESERVE THESE STYLES IN THE HEAD\r\n        ------------------------------------- */\r\n        @media all {\r\n            .ExternalClass {\r\n                width: 100%;\r\n            }\r\n\r\n                .ExternalClass,\r\n                .ExternalClass p,\r\n                .ExternalClass span,\r\n                .ExternalClass font,\r\n                .ExternalClass td,\r\n                .ExternalClass div {\r\n                    line-height: 100%;\r\n                }\r\n\r\n            .apple-link a {\r\n                color: inherit !important;\r\n                font-family: inherit !important;\r\n                font-size: inherit !important;\r\n                font-weight: inherit !important;\r\n                line-height: inherit !important;\r\n                text-decoration: none !important;\r\n            }\r\n        }\r\n    </style>\r\n</head>\r\n<body class=\"\">\r\n    <div style=\"background-color: #389f3e;\">\r\n        <!--[if gte mso 9]>\r\n        <v:background xmlns:v=\"urn:schemas-microsoft-com:vml\" fill=\"t\">\r\n          <v:fill type=\"tile\" src=\"@Model.AppUrl/Content/images/bg-img.png\" color=\"#33b5e5\"/>\r\n        </v:background>\r\n        <![endif]-->\r\n        <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" background=\"@Model.AppUrl/Content/images/bg-img.png\" class=\"body\">\r\n            <tr>\r\n                <td valign=\"top\" align=\"left\" class=\"container\">\r\n                    <div class=\"content\">\r\n\r\n                        <!-- START CENTERED WHITE CONTAINER -->\r\n                        <span class=\"preheader\">@Model.Subject</span>\r\n                        <table class=\"main\">\r\n\r\n                            <!-- START MAIN CONTENT AREA -->\r\n                            <tr>\r\n                                <td class=\"wrapper\">\r\n                                    <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\">\r\n                                        <tr>\r\n                                            <td>\r\n                                                <h2>\r\n                                                    <b>@Model.Subject</b><br />\r\n                                                    <small>\r\n                                                        <a href=\"@Model.AppUrl\" target=\"_blank\">@Model.AppUrl</a>\r\n                                                    </small>\r\n                                                </h2>\r\n                                                <p>\r\n                                                    @Model.Name, witamy na portalu ludzi kopiących się po kostkach! Kliknij powyższy link, aby zakończyć rejestrację.\r\n                                                </p>\r\n                                                <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"btn btn-primary\">\r\n                                                    <tbody>\r\n                                                        <tr>\r\n                                                            <td align=\"left\">\r\n                                                                <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\">\r\n                                                                    <tbody>\r\n                                                                        <tr>\r\n                                                                            <td> <a href=\"@Model.AppUrl\" target=\"_blank\">ZALOGUJ</a> </td>\r\n                                                                        </tr>\r\n                                                                    </tbody>\r\n                                                                </table>\r\n                                                            </td>\r\n                                                        </tr>\r\n                                                    </tbody>\r\n                                                </table>\r\n                                                <p>\r\n                                            </td>\r\n                                        </tr>\r\n                                    </table>\r\n                                </td>\r\n                            </tr>\r\n\r\n                            <!-- END MAIN CONTENT AREA -->\r\n                        </table>\r\n\r\n                        <!-- START FOOTER -->\r\n                        <div class=\"footer\">\r\n                            <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\">\r\n                                <tr>\r\n                                    <td class=\"content-block\">\r\n                                        <span class=\"apple-link\">Soccer</span>\r\n                                    </td>\r\n                                </tr>\r\n                            </table>\r\n                        </div>\r\n                        <!-- END FOOTER -->\r\n                        <!-- END CENTERED WHITE CONTAINER -->\r\n                    </div>\r\n                </td>\r\n            </tr>\r\n        </table>\r\n    </div>\r\n</body>\r\n</html>", "@Model.Name - witaj na portalu Soccer", 1, 2 });

            migrationBuilder.InsertData(
                table: "TranslationEntries",
                columns: new[] { "Id", "LanguageId", "Text", "TranslationId" },
                values: new object[] { 1, 2, "<!doctype html>\r\n<html>\r\n<head>\r\n    <meta name=\"viewport\" content=\"width=device-width\" />\r\n    <meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\" />\r\n    <title>@Model.Subject</title>\r\n    <style>\r\n        /* -------------------------------------\r\n            GLOBAL RESETS\r\n        ------------------------------------- */\r\n        img {\r\n            border: none;\r\n            -ms-interpolation-mode: bicubic;\r\n            max-width: 100%;\r\n        }\r\n\r\n        body {\r\n            font-family: sans-serif;\r\n            -webkit-font-smoothing: antialiased;\r\n            font-size: 14px;\r\n            line-height: 1.4;\r\n            margin: 0;\r\n            padding: 0;\r\n            -ms-text-size-adjust: 100%;\r\n            -webkit-text-size-adjust: 100%;\r\n            height: 100%;\r\n            width: 100%;\r\n        }\r\n\r\n        table {\r\n            border-collapse: separate;\r\n            mso-table-lspace: 0pt;\r\n            mso-table-rspace: 0pt;\r\n            width: 100%;\r\n        }\r\n\r\n            table td {\r\n                font-family: sans-serif;\r\n                font-size: 14px;\r\n                vertical-align: top;\r\n            }\r\n        /* -------------------------------------\r\n            BODY & CONTAINER\r\n        ------------------------------------- */\r\n        html {\r\n            height: 100%;\r\n        }\r\n\r\n        .body {\r\n            position: relative;\r\n            z-index: 9999;\r\n        }\r\n\r\n        /* Set a max-width, and make it display as block so it will automatically stretch to that width, but will also shrink down on a phone or something */\r\n        .container {\r\n            display: block;\r\n            Margin: 0 auto !important;\r\n            /* makes it centered */\r\n            max-width: 580px;\r\n            padding: 30px 10px;\r\n            width: 580px;\r\n        }\r\n        /* This should also be a block element, so that it will fill 100% of the .container */\r\n        .content {\r\n            box-sizing: border-box;\r\n            display: block;\r\n            Margin: 0 auto;\r\n            max-width: 580px;\r\n            padding: 10px;\r\n        }\r\n        /* -------------------------------------\r\n            HEADER, FOOTER, MAIN\r\n        ------------------------------------- */\r\n        .main {\r\n            background: #fff;\r\n            border-radius: 3px;\r\n            width: 100%;\r\n        }\r\n\r\n        .wrapper {\r\n            box-sizing: border-box;\r\n            padding: 20px;\r\n        }\r\n\r\n        .footer {\r\n            clear: both;\r\n            padding-top: 10px;\r\n            text-align: center;\r\n            width: 100%;\r\n        }\r\n\r\n            .footer td,\r\n            .footer p,\r\n            .footer span,\r\n            .footer a {\r\n                color: #fff;\r\n                font-size: 12px;\r\n                text-align: center;\r\n            }\r\n        /* -------------------------------------\r\n            TYPOGRAPHY\r\n        ------------------------------------- */\r\n        h1,\r\n        h2,\r\n        h3,\r\n        h4 {\r\n            color: #000000;\r\n            font-family: sans-serif;\r\n            font-weight: 400;\r\n            line-height: 1.4;\r\n            margin: 0;\r\n            Margin-bottom: 20px;\r\n        }\r\n\r\n        h1 {\r\n            font-size: 35px;\r\n            font-weight: 300;\r\n            text-align: center;\r\n            text-transform: capitalize;\r\n        }\r\n\r\n        p,\r\n        ul,\r\n        ol {\r\n            font-family: sans-serif;\r\n            font-size: 14px;\r\n            font-weight: normal;\r\n            margin: 0;\r\n            Margin-bottom: 15px;\r\n        }\r\n\r\n            p li,\r\n            ul li,\r\n            ol li {\r\n                list-style-position: inside;\r\n                margin-left: 5px;\r\n            }\r\n\r\n        a {\r\n            color: #3498db;\r\n            text-decoration: underline;\r\n        }\r\n        /* -------------------------------------\r\n            BUTTONS\r\n        ------------------------------------- */\r\n        .btn {\r\n            box-sizing: border-box;\r\n            width: 100%;\r\n        }\r\n\r\n            .btn > tbody > tr > td {\r\n                padding-bottom: 15px;\r\n            }\r\n\r\n            .btn table {\r\n                width: auto;\r\n            }\r\n\r\n                .btn table td {\r\n                    background-color: #ffffff;\r\n                    border-radius: 5px;\r\n                    text-align: center;\r\n                }\r\n\r\n            .btn a {\r\n                background-color: #ffffff;\r\n                border: solid 1px #3498db;\r\n                box-sizing: border-box;\r\n                color: #3498db;\r\n                cursor: pointer;\r\n                display: inline-block;\r\n                font-size: 16px;\r\n                font-weight: bold;\r\n                margin: 0;\r\n                padding: 12px 25px;\r\n                text-decoration: none;\r\n                text-transform: capitalize;\r\n            }\r\n\r\n        .btn-primary a {\r\n            border: 3px solid #00c851;\r\n            color: #00c851;\r\n        }\r\n        /* -------------------------------------\r\n            OTHER STYLES THAT MIGHT BE USEFUL\r\n        ------------------------------------- */\r\n        .last {\r\n            margin-bottom: 0;\r\n        }\r\n\r\n        .first {\r\n            margin-top: 0;\r\n        }\r\n\r\n        .align-center {\r\n            text-align: center;\r\n        }\r\n\r\n        .align-right {\r\n            text-align: right;\r\n        }\r\n\r\n        .align-left {\r\n            text-align: left;\r\n        }\r\n\r\n        .clear {\r\n            clear: both;\r\n        }\r\n\r\n        .mt0 {\r\n            margin-top: 0;\r\n        }\r\n\r\n        .mb0 {\r\n            margin-bottom: 0;\r\n        }\r\n\r\n        .preheader {\r\n            color: transparent;\r\n            display: none;\r\n            height: 0;\r\n            max-height: 0;\r\n            max-width: 0;\r\n            opacity: 0;\r\n            overflow: hidden;\r\n            mso-hide: all;\r\n            visibility: hidden;\r\n            width: 0;\r\n        }\r\n\r\n        .powered-by a {\r\n            text-decoration: none;\r\n        }\r\n\r\n        hr {\r\n            border: 0;\r\n            border-bottom: 1px solid #f6f6f6;\r\n            Margin: 20px 0;\r\n        }\r\n        /* -------------------------------------\r\n            RESPONSIVE AND MOBILE FRIENDLY STYLES\r\n        ------------------------------------- */\r\n        @media only screen and (max-width: 620px) {\r\n            table[class=body] h1 {\r\n                font-size: 28px !important;\r\n                margin-bottom: 10px !important;\r\n            }\r\n\r\n            table[class=body] p,\r\n            table[class=body] ul,\r\n            table[class=body] ol,\r\n            table[class=body] td,\r\n            table[class=body] span,\r\n            table[class=body] a {\r\n                font-size: 16px !important;\r\n            }\r\n\r\n            table[class=body] .wrapper,\r\n            table[class=body] .article {\r\n                padding: 10px !important;\r\n            }\r\n\r\n            table[class=body] .content {\r\n                padding: 0 !important;\r\n            }\r\n\r\n            table[class=body] .container {\r\n                padding: 0 !important;\r\n                width: 100% !important;\r\n            }\r\n\r\n            table[class=body] .main {\r\n                border-left-width: 0 !important;\r\n                border-radius: 0 !important;\r\n                border-right-width: 0 !important;\r\n            }\r\n\r\n            table[class=body] .btn table {\r\n                width: 100% !important;\r\n            }\r\n\r\n            table[class=body] .btn a {\r\n                width: 100% !important;\r\n            }\r\n\r\n            table[class=body] .img-responsive {\r\n                height: auto !important;\r\n                max-width: 100% !important;\r\n                width: auto !important;\r\n            }\r\n\r\n            table[class=body] .img-responsive {\r\n                height: auto !important;\r\n                max-width: 100% !important;\r\n                width: auto !important;\r\n            }\r\n\r\n            table[class=body] img {\r\n                text-align: center;\r\n            }\r\n        }\r\n        /* -------------------------------------\r\n            PRESERVE THESE STYLES IN THE HEAD\r\n        ------------------------------------- */\r\n        @media all {\r\n            .ExternalClass {\r\n                width: 100%;\r\n            }\r\n\r\n                .ExternalClass,\r\n                .ExternalClass p,\r\n                .ExternalClass span,\r\n                .ExternalClass font,\r\n                .ExternalClass td,\r\n                .ExternalClass div {\r\n                    line-height: 100%;\r\n                }\r\n\r\n            .apple-link a {\r\n                color: inherit !important;\r\n                font-family: inherit !important;\r\n                font-size: inherit !important;\r\n                font-weight: inherit !important;\r\n                line-height: inherit !important;\r\n                text-decoration: none !important;\r\n            }\r\n        }\r\n    </style>\r\n</head>\r\n<body class=\"\">\r\n    <div style=\"background-color: #389f3e;\">\r\n        <!--[if gte mso 9]>\r\n        <v:background xmlns:v=\"urn:schemas-microsoft-com:vml\" fill=\"t\">\r\n          <v:fill type=\"tile\" src=\"@Model.AppUrl/Content/images/bg-img.png\" color=\"#33b5e5\"/>\r\n        </v:background>\r\n        <![endif]-->\r\n        <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" background=\"@Model.AppUrl/Content/images/bg-img.png\" class=\"body\">\r\n            <tr>\r\n                <td valign=\"top\" align=\"left\" class=\"container\">\r\n                    <div class=\"content\">\r\n\r\n                        <!-- START CENTERED WHITE CONTAINER -->\r\n                        <span class=\"preheader\">@Model.Subject</span>\r\n                        <table class=\"main\">\r\n\r\n                            <!-- START MAIN CONTENT AREA -->\r\n                            <tr>\r\n                                <td class=\"wrapper\">\r\n                                    <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\">\r\n                                        <tr>\r\n                                            <td>\r\n                                                <h2>\r\n                                                    <b>@Model.Subject</b><br />\r\n                                                    <small>\r\n                                                        <a href=\"@Model.AppUrl\" target=\"_blank\">@Model.AppUrl</a>\r\n                                                    </small>\r\n                                                </h2>\r\n                                                <p>\r\n                                                    @Model.Name, welcome on our site!\r\n                                                </p>\r\n                                                <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"btn btn-primary\">\r\n                                                    <tbody>\r\n                                                        <tr>\r\n                                                            <td align=\"left\">\r\n                                                                <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\">\r\n                                                                    <tbody>\r\n                                                                        <tr>\r\n                                                                            <td> <a href=\"@Model.AppUrl\" target=\"_blank\">Log in</a> </td>\r\n                                                                        </tr>\r\n                                                                    </tbody>\r\n                                                                </table>\r\n                                                            </td>\r\n                                                        </tr>\r\n                                                    </tbody>\r\n                                                </table>\r\n                                                <p>\r\n                                            </td>\r\n                                        </tr>\r\n                                    </table>\r\n                                </td>\r\n                            </tr>\r\n\r\n                            <!-- END MAIN CONTENT AREA -->\r\n                        </table>\r\n\r\n                        <!-- START FOOTER -->\r\n                        <div class=\"footer\">\r\n                            <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\">\r\n                                <tr>\r\n                                    <td class=\"content-block\">\r\n                                        <span class=\"apple-link\">Soccer</span>\r\n                                    </td>\r\n                                </tr>\r\n                            </table>\r\n                        </div>\r\n                        <!-- END FOOTER -->\r\n                        <!-- END CENTERED WHITE CONTAINER -->\r\n                    </div>\r\n                </td>\r\n            </tr>\r\n        </table>\r\n    </div>\r\n</body>\r\n</html>", 1 });

            migrationBuilder.InsertData(
                table: "TranslationEntries",
                columns: new[] { "Id", "LanguageId", "Text", "TranslationId" },
                values: new object[] { 2, 2, "@Model.Name - welcome to Soccer portal!", 2 });

            migrationBuilder.CreateIndex(
                name: "IX_Templates_TranslateContentId",
                table: "Templates",
                column: "TranslateContentId");

            migrationBuilder.CreateIndex(
                name: "IX_Templates_TranslateHeaderId",
                table: "Templates",
                column: "TranslateHeaderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Emails");

            migrationBuilder.DropTable(
                name: "Templates");

            migrationBuilder.DeleteData(
                table: "TranslationEntries",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TranslationEntries",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
