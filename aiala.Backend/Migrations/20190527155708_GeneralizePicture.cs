using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace aiala.Backend.Migrations
{
    public partial class GeneralizePicture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PicturesTags",
                schema: "aiala");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "aiala",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "DescriptionConfidence",
                schema: "aiala",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "HasHumanConfidence",
                schema: "aiala",
                table: "Pictures");

            migrationBuilder.AddColumn<string>(
                name: "CallingCode",
                schema: "directory",
                table: "Countries",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "PictureId",
                schema: "aiala",
                table: "ScheduledPlace",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "PictureId",
                schema: "aiala",
                table: "Places",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.CreateTable(
                name: "AiPictureMetadatas",
                schema: "aiala",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    HasHumanConfidence = table.Column<bool>(nullable: false),
                    DescriptionConfidence = table.Column<float>(nullable: false),
                    PictureId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AiPictureMetadatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AiPictureMetadatas_Pictures_PictureId",
                        column: x => x.PictureId,
                        principalSchema: "aiala",
                        principalTable: "Pictures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AiPictureTags",
                schema: "aiala",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    HasHumanConfidence = table.Column<bool>(nullable: false),
                    Confidence = table.Column<float>(nullable: false),
                    PictureId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AiPictureTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AiPictureTags_AiPictureMetadatas_PictureId",
                        column: x => x.PictureId,
                        principalSchema: "aiala",
                        principalTable: "AiPictureMetadatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "directory",
                table: "Countries",
                columns: new[] { "Id", "CallingCode", "Code", "Enabled", "Name" },
                values: new object[,]
                {
                    { new Guid("44e996a7-fb2c-41bf-917f-02f3dd79f639"), "+93", "AF", true, "Afghanistan" },
                    { new Guid("c41c146e-66c6-4b79-83bc-d18117231f31"), "+687", "NC", true, "New Caledonia" },
                    { new Guid("7659bcc4-41cf-4846-bab8-2bae6f938359"), "+64", "NZ", true, "New Zealand" },
                    { new Guid("2a032347-4030-4119-babc-95e1da3e8e16"), "+505", "NI", true, "Nicaragua" },
                    { new Guid("c00c5c0b-c6be-4d27-b4db-302443d57439"), "+227", "NE", true, "Niger" },
                    { new Guid("a53ee786-5790-445b-95cc-cdf563cb599e"), "+234", "NG", true, "Nigeria" },
                    { new Guid("aef0117a-7970-4a60-9fc1-279c8c8b6eb6"), "+683", "NU", true, "Niue" },
                    { new Guid("158eb17f-f08d-42cc-95a4-057f7133ae51"), "+672", "NF", true, "Norfolk Island" },
                    { new Guid("c50719ef-6ccb-41d7-9244-5c82dde5e804"), "+850", "KP", true, "Korea (Democratic People's Republic of)" },
                    { new Guid("1b6384c7-92a9-42b3-b996-c0b134ab1170"), "+1670", "MP", true, "Northern Mariana Islands" },
                    { new Guid("3abddad7-fa8f-456b-a590-33c5083b98dd"), "+47", "NO", true, "Norway" },
                    { new Guid("be94fb67-a08c-419f-a9bc-dda56d4b5096"), "+968", "OM", true, "Oman" },
                    { new Guid("5c62dbd2-c02e-4ae9-8b14-031c6c874659"), "+92", "PK", true, "Pakistan" },
                    { new Guid("cc76e0b5-40d7-40cc-ac59-c6dcd1b29cf0"), "+680", "PW", true, "Palau" },
                    { new Guid("e6e85a31-ae53-43ee-b21a-f6ed0538b020"), "+970", "PS", true, "Palestine, State of" },
                    { new Guid("64197b44-3373-4b0f-ada8-64a0066029ff"), "+507", "PA", true, "Panama" },
                    { new Guid("da4bd7d8-23a5-4a40-804f-e9a67cfa93a6"), "+675", "PG", true, "Papua New Guinea" },
                    { new Guid("c5f5f3ee-f4e1-4569-8e92-58a2b4a569cb"), "+595", "PY", true, "Paraguay" },
                    { new Guid("994c8be2-8fa3-43ee-a725-41895002f499"), "+51", "PE", true, "Peru" },
                    { new Guid("04549a74-b458-4bc6-94de-d339d5ed12e1"), "+63", "PH", true, "Philippines" },
                    { new Guid("7fbe832c-04f2-4f09-932c-af642d295032"), "+64", "PN", true, "Pitcairn" },
                    { new Guid("c92e3b7f-e7aa-4b23-bd38-8ef653b7f7a2"), "+48", "PL", true, "Poland" },
                    { new Guid("1855fe95-33ad-4a68-bcc3-c57ed85ce329"), "+351", "pt", true, "Portugal" },
                    { new Guid("ac050c4f-a0bc-4e0d-88ca-2853b9438d85"), "+1787", "PR", true, "Puerto Rico" },
                    { new Guid("ac714d22-4b36-4776-b842-728d72319087"), "+974", "QA", true, "Qatar" },
                    { new Guid("d55e3ff4-05e5-41f3-9e63-7a330f920950"), "+383", "XK", true, "Republic of Kosovo" },
                    { new Guid("7401ab85-99a8-40bd-b5b0-c4d94d25aa69"), "+262", "RE", true, "Réunion" },
                    { new Guid("1d27b72f-31d6-4568-86cc-7b6ae7e206a3"), "+40", "RO", true, "Romania" },
                    { new Guid("c8ac2c56-36fd-40f2-9ea9-6974f2387a0e"), "+31", "nl", true, "Netherlands" },
                    { new Guid("302ecafb-2dcb-466c-ab01-cf12d7b1544e"), "+7", "RU", true, "Russian Federation" },
                    { new Guid("15d6f9ea-1026-4ad3-88f8-f3e8f1eb6f6e"), "+977", "NP", true, "Nepal" },
                    { new Guid("ecd1a960-7846-4599-b78c-ae0bbfbf8f54"), "+264", "NA", true, "Namibia" },
                    { new Guid("c4fb5e61-c37f-4e5f-b70b-65c7ff3a24b8"), "+218", "LY", true, "Libya" },
                    { new Guid("2a7a9ede-fcf3-4455-9cd2-daaf2f07851d"), "+423", "LI", true, "Liechtenstein" },
                    { new Guid("08051e14-d13e-4e9e-90a6-0f3c4fc7b6d8"), "+370", "LT", true, "Lithuania" },
                    { new Guid("363ae089-3a30-478d-b49f-5c7b9bdafe50"), "+352", "LU", true, "Luxembourg" },
                    { new Guid("0e14972b-4f7c-4d6d-938b-ca6360b0872b"), "+853", "MO", true, "Macao" },
                    { new Guid("39035db4-10a0-4070-9fa6-b703a9a79199"), "+389", "MK", true, "Macedonia (the former Yugoslav Republic of)" },
                    { new Guid("603218e7-8773-44ef-a1de-34b7d5d7b21e"), "+261", "MG", true, "Madagascar" },
                    { new Guid("b07d1ada-7a09-484e-aa77-97ce1b769ac7"), "+265", "MW", true, "Malawi" },
                    { new Guid("c895cb3b-0be9-44e5-8bd9-3c5ddbeac938"), "+60", "MY", true, "Malaysia" },
                    { new Guid("39dcf65d-b256-4c48-9ffe-2cfd92d9adb1"), "+960", "MV", true, "Maldives" },
                    { new Guid("d6ecbf46-a0a5-4e61-89f9-c9acd0782532"), "+223", "ML", true, "Mali" },
                    { new Guid("dc48c0ec-8c3b-4d06-9283-935fa5453730"), "+356", "MT", true, "Malta" },
                    { new Guid("c2195d27-4ca3-46f9-b289-8250e32dd019"), "+692", "MH", true, "Marshall Islands" },
                    { new Guid("cb7d0d8e-7612-44b6-a1d6-9ac33762a823"), "+596", "MQ", true, "Martinique" },
                    { new Guid("02778799-6b45-47be-923f-7a1cd2e287a2"), "+222", "MR", true, "Mauritania" },
                    { new Guid("76819034-2d0b-4296-bb93-4c61927f9d8d"), "+230", "MU", true, "Mauritius" },
                    { new Guid("9e974584-f073-415c-b535-9bf6f8f83fbc"), "+262", "YT", true, "Mayotte" },
                    { new Guid("23440164-15e7-4aa2-ba50-6699b81b31ff"), "+52", "MX", true, "Mexico" },
                    { new Guid("8328c807-7bb3-4c7e-82d8-d72b0c148204"), "+691", "FM", true, "Micronesia (Federated States of)" },
                    { new Guid("80ddc377-d6d3-40d0-aa85-b17907310d2b"), "+373", "MD", true, "Moldova (Republic of)" },
                    { new Guid("f7dcbb8a-90d6-4008-89f9-fa8cd241d66f"), "+377", "MC", true, "Monaco" },
                    { new Guid("2c691e17-979b-4192-a106-3ccc4da056a6"), "+976", "MN", true, "Mongolia" },
                    { new Guid("a2141dc6-5c4b-400f-bdb1-211ac5081954"), "+382", "ME", true, "Montenegro" },
                    { new Guid("0da60206-a3f5-4a18-a8b6-630e12719c82"), "+1664", "MS", true, "Montserrat" },
                    { new Guid("d8b3c897-69e1-4bd0-aecf-668b0dadffc7"), "+212", "MA", true, "Morocco" },
                    { new Guid("ce56bc91-b97f-4f14-8c02-7e4ec2c6b1be"), "+258", "MZ", true, "Mozambique" },
                    { new Guid("cbb6a6af-e1e0-4d6d-a908-58b8f130576e"), "+95", "MM", true, "Myanmar" },
                    { new Guid("8cce699e-b033-4b9b-902e-6483da7ca19a"), "+674", "NR", true, "Nauru" },
                    { new Guid("bdbdeb23-9199-4a6c-b0f5-33014965fe40"), "+250", "RW", true, "Rwanda" },
                    { new Guid("afc0c551-cec0-4999-a9fe-7f92710785ac"), "+590", "BL", true, "Saint Barthélemy" },
                    { new Guid("fcdc7d01-ea17-4ac8-b712-f778ee2de9f2"), "+290", "SH", true, "Saint Helena, Ascension and Tristan da Cunha" },
                    { new Guid("f1e7967a-0237-4ec2-95bc-f92fb9cf5537"), "+886", "TW", true, "Taiwan" },
                    { new Guid("e796d639-b073-4de5-b761-907ea7fc4ffc"), "+992", "TJ", true, "Tajikistan" },
                    { new Guid("22936236-016e-4c06-aff9-d33d56c7c901"), "+255", "TZ", true, "Tanzania, United Republic of" },
                    { new Guid("5198fbd1-17c2-4130-b3ba-377c37c893af"), "+66", "TH", true, "Thailand" },
                    { new Guid("431af217-da9c-49c0-862d-346c3f154807"), "+670", "TL", true, "Timor-Leste" },
                    { new Guid("91296619-d79b-4f9f-b0b1-5356f17bf26e"), "+228", "TG", true, "Togo" },
                    { new Guid("6022bae6-98fd-4aa6-8900-b747f6c35044"), "+690", "TK", true, "Tokelau" },
                    { new Guid("7945287d-cd87-4153-a98d-341a50f701bf"), "+676", "TO", true, "Tonga" },
                    { new Guid("68f56a68-74d2-4ff9-99cb-a946d96edd1b"), "+1868", "TT", true, "Trinidad and Tobago" },
                    { new Guid("6a1bb25b-a7c8-4a72-bbfa-e3e7f0a7112b"), "+216", "TN", true, "Tunisia" },
                    { new Guid("7b1130ba-25c5-4886-8683-9955f720c67e"), "+90", "TR", true, "Turkey" },
                    { new Guid("6aa8a2e4-5b28-4fc0-a1ad-e80ad2426ede"), "+993", "TM", true, "Turkmenistan" },
                    { new Guid("e445207e-d79a-40b1-a108-3f6f8a37d503"), "+1649", "TC", true, "Turks and Caicos Islands" },
                    { new Guid("1eb5343a-b090-4efc-a684-ba5d5a613516"), "+688", "TV", true, "Tuvalu" },
                    { new Guid("d2fc318d-ef9a-49d3-8db3-1264b8415728"), "+256", "UG", true, "Uganda" },
                    { new Guid("bb691dd6-b2e1-4a61-8329-58f233d66d3a"), "+380", "UA", true, "Ukraine" },
                    { new Guid("54d6b82d-49da-44a5-8387-44eb555c8d44"), "+971", "AE", true, "United Arab Emirates" },
                    { new Guid("9f53e4c4-bc1e-4d26-b0a0-d1d589f9e443"), "+44", "GB", true, "United Kingdom of Great Britain and Northern Ireland" },
                    { new Guid("b54fac30-8dbd-4c34-921b-ecb780d45e05"), "+1", "US", true, "United States of America" },
                    { new Guid("05e5ab87-dc04-404e-925d-ac6c90d82427"), "+598", "UY", true, "Uruguay" },
                    { new Guid("83e157d8-1021-4d51-9a4a-2d627e769456"), "+998", "UZ", true, "Uzbekistan" },
                    { new Guid("dd4b36b5-4499-48c7-ae89-b8f18c4b6fb9"), "+678", "VU", true, "Vanuatu" },
                    { new Guid("feb9d6d5-2327-4065-9cbc-a80d0b0ebc89"), "+58", "VE", true, "Venezuela (Bolivarian Republic of)" },
                    { new Guid("c046fa0d-300b-4d38-b811-34aebae66b83"), "+84", "VN", true, "Viet Nam" },
                    { new Guid("7973277f-00e4-4518-be78-a811f244bed5"), "+681", "WF", true, "Wallis and Futuna" },
                    { new Guid("12d54c0e-6444-43fb-b27e-d82e319480e3"), "+212", "EH", true, "Western Sahara" },
                    { new Guid("8f532a74-0192-43ce-88e2-b57335b1daaf"), "+967", "YE", true, "Yemen" },
                    { new Guid("bc198476-8c8b-433e-80ad-c78d93b65758"), "+963", "SY", true, "Syrian Arab Republic" },
                    { new Guid("f7fd2d44-31cc-4b71-8afd-cedf532cc605"), "+41", "CH", true, "Switzerland" },
                    { new Guid("453ffcfa-6e29-41db-99a1-72d0067abf30"), "+46", "SE", true, "Sweden" },
                    { new Guid("900794cd-2b60-41da-97ae-c7bc082f9cf9"), "+268", "SZ", true, "Swaziland" },
                    { new Guid("fcb3d8e1-2a9c-470f-b7c5-15dea06477c9"), "+1869", "KN", true, "Saint Kitts and Nevis" },
                    { new Guid("e0be6610-da6e-4d3d-8699-8825fb4ee337"), "+1758", "LC", true, "Saint Lucia" },
                    { new Guid("bba868d5-0661-4421-8141-b13297e96218"), "+590", "MF", true, "Saint Martin (French part)" },
                    { new Guid("a0abe09e-395c-48d3-bef7-3f9282b1c60d"), "+508", "PM", true, "Saint Pierre and Miquelon" },
                    { new Guid("7f8afd92-61bd-477c-bf78-12d62b3e5cb5"), "+1784", "VC", true, "Saint Vincent and the Grenadines" },
                    { new Guid("64684184-0951-47e4-bac3-282314ccc2bd"), "+685", "WS", true, "Samoa" },
                    { new Guid("f59c01d4-367d-4773-8c4c-2b47e4f2e3a0"), "+378", "SM", true, "San Marino" },
                    { new Guid("c052a003-3341-442e-825b-fcbc3c208498"), "+239", "ST", true, "Sao Tome and Principe" },
                    { new Guid("2a7a8f6b-dda3-4799-8491-f4d03825899a"), "+966", "SA", true, "Saudi Arabia" },
                    { new Guid("c3cdf8e2-45c4-4f11-a91d-fa1a43bcd9f9"), "+221", "SN", true, "Senegal" },
                    { new Guid("f2fde10e-0e24-4a72-9f1f-2c248bae340c"), "+381", "RS", true, "Serbia" },
                    { new Guid("36a1b84a-849d-4fb1-ba49-8b8d85011554"), "+248", "SC", true, "Seychelles" },
                    { new Guid("142dac4e-e495-4c29-aaac-5aa6c2b2f630"), "+232", "SL", true, "Sierra Leone" },
                    { new Guid("c2473d84-7c6b-4b32-a624-77419bef5104"), "+231", "LR", true, "Liberia" },
                    { new Guid("ede02c3f-9b24-4d85-a0c7-021e5386c861"), "+65", "SG", true, "Singapore" },
                    { new Guid("efa9f8fb-92f8-46da-8171-b2947811b8cf"), "+421", "SK", true, "Slovakia" },
                    { new Guid("5ad56577-7098-4515-b010-06324670a069"), "+386", "SI", true, "Slovenia" },
                    { new Guid("871803b6-865a-4aee-8e4d-95189da19c50"), "+677", "SB", true, "Solomon Islands" },
                    { new Guid("6aeaa091-8c4b-47a9-94f0-6e2b9636bf72"), "+252", "SO", true, "Somalia" },
                    { new Guid("4474dd19-7cff-4df2-9de6-f1f52b9aa94c"), "+27", "ZA", true, "South Africa" },
                    { new Guid("3fdd5f8d-f594-41d3-aeb6-ffabcfe62f36"), "+500", "GS", true, "South Georgia and the South Sandwich Islands" },
                    { new Guid("8a951ed8-ca8c-4e8a-8232-d728d8a8a3e5"), "+82", "KR", true, "Korea (Republic of)" },
                    { new Guid("a9501973-f190-471f-b299-acd6085ccfbc"), "+211", "SS", true, "South Sudan" },
                    { new Guid("c6b48dac-ceaf-45f6-a71c-70dcb28e3dd3"), "+34", "es", true, "Spain" },
                    { new Guid("20b93646-a854-4cbc-9e45-f32cfe536f00"), "+94", "LK", true, "Sri Lanka" },
                    { new Guid("c355b129-8496-4f66-b39b-a05b1e140f24"), "+249", "SD", true, "Sudan" },
                    { new Guid("0b153c4a-481f-4cbb-a488-d8e71ea05ff3"), "+597", "SR", true, "Suriname" },
                    { new Guid("0516741a-702a-4c5a-9b7e-92f21356b336"), "+4779", "SJ", true, "Svalbard and Jan Mayen" },
                    { new Guid("0bf9b7ac-0e84-41bd-8e97-b81352e451f2"), "+1721", "SX", true, "Sint Maarten (Dutch part)" },
                    { new Guid("81a28038-c55d-4eca-8e68-668b2864fd98"), "+266", "LS", true, "Lesotho" },
                    { new Guid("743de50e-6fa1-4272-97e1-06bc862fe505"), "+961", "LB", true, "Lebanon" },
                    { new Guid("ae7b0ee4-e00f-436d-a9cc-55eb493a392f"), "+371", "LV", true, "Latvia" },
                    { new Guid("d83ca70f-d185-4c87-b82a-6cba91de7531"), "+", "UM", true, "United States Minor Outlying Islands" },
                    { new Guid("f2c63602-3db9-401f-a8bc-e352359dd77d"), "+1284", "VG", true, "Virgin Islands (British)" },
                    { new Guid("b21c14d5-09e3-4f22-952e-f739db8b2dc0"), "+1 340", "VI", true, "Virgin Islands (U.S.)" },
                    { new Guid("234e687b-9acf-4268-9d64-1b622f43e673"), "+673", "BN", true, "Brunei Darussalam" },
                    { new Guid("db9524e7-418a-47ca-a207-982af4c59666"), "+359", "BG", true, "Bulgaria" },
                    { new Guid("8216f3c7-2a58-42e8-acd4-308021850354"), "+226", "BF", true, "Burkina Faso" },
                    { new Guid("56aca540-4ae2-43cf-bdff-72ea9b868780"), "+257", "BI", true, "Burundi" },
                    { new Guid("9651830d-20e4-4222-aa36-1b596500670d"), "+855", "KH", true, "Cambodia" },
                    { new Guid("dda9bd73-0cb2-423d-adcc-ee0d393b22b5"), "+237", "CM", true, "Cameroon" },
                    { new Guid("f83ba14a-2758-45ef-93df-10170b0759c3"), "+1", "CA", true, "Canada" },
                    { new Guid("bf5f1f0f-9b12-41a4-a1aa-438c6a3af860"), "+238", "CV", true, "Cabo Verde" },
                    { new Guid("aa85fc2b-e3a5-4849-b869-724d61bc383e"), "+1345", "KY", true, "Cayman Islands" },
                    { new Guid("f30caf70-5235-4f4d-b300-c63efa33b1b1"), "+236", "CF", true, "Central African Republic" },
                    { new Guid("7cf19a97-a4f6-4a07-8dee-8175b1c3cd7d"), "+235", "TD", true, "Chad" },
                    { new Guid("668e3ca5-8bfb-4f8c-8c98-207e3889f0fe"), "+56", "CL", true, "Chile" },
                    { new Guid("8e6e14e7-d429-4837-bb2b-e84f6cc3299a"), "+86", "CN", true, "China" },
                    { new Guid("a349e604-c2d1-403f-82ec-01c41c26825e"), "+61", "CX", true, "Christmas Island" },
                    { new Guid("c7524e4a-6065-4fa6-a33b-215ae8f87e86"), "+61", "CC", true, "Cocos (Keeling) Islands" },
                    { new Guid("4cdcb59b-7021-4c86-8fa2-87b807460ad9"), "+57", "CO", true, "Colombia" },
                    { new Guid("50edd070-117c-4b0a-ad32-97607b3b4fb5"), "+269", "KM", true, "Comoros" },
                    { new Guid("0fe76a29-bd01-4578-b770-f1f51dbf7172"), "+242", "CG", true, "Congo" },
                    { new Guid("f28a2a31-b33e-410a-8418-5f84240a37d2"), "+243", "CD", true, "Congo (Democratic Republic of the)" },
                    { new Guid("47b942da-f582-4a85-9347-edf92e14ea86"), "+682", "CK", true, "Cook Islands" },
                    { new Guid("3fea9a2d-c0ef-40a3-8128-c589e95c3537"), "+506", "CR", true, "Costa Rica" },
                    { new Guid("9c22df3d-bbbf-4a8a-a0ef-b1720bc43463"), "+385", "hr", true, "Croatia" },
                    { new Guid("583cb448-7e2d-4c30-9835-343bbf977e9b"), "+53", "CU", true, "Cuba" },
                    { new Guid("31387bd8-b5d7-41c0-9807-d39d3a4453dc"), "+599", "CW", true, "Curaçao" },
                    { new Guid("2e38cb7e-7dc3-40a7-a947-533fd1134105"), "+246", "IO", true, "British Indian Ocean Territory" },
                    { new Guid("d6a33871-b015-4015-ac21-9a95ae8c89e5"), "+55", "br", true, "Brazil" },
                    { new Guid("9d19efbd-70e8-4817-afbb-fb735ff78ad7"), "+", "BV", true, "Bouvet Island" },
                    { new Guid("92414cfd-4ba3-469d-b951-3ff06eecffdc"), "+267", "BW", true, "Botswana" },
                    { new Guid("0b4f312f-3456-4cc9-ba85-acb48b9e7e9e"), "+358", "AX", true, "Åland Islands" },
                    { new Guid("7a551b34-cb89-40bd-820e-7bf0c5603536"), "+355", "AL", true, "Albania" },
                    { new Guid("0a1fadde-e103-40f3-94f8-ae58e70ee91d"), "+213", "DZ", true, "Algeria" },
                    { new Guid("f0b72653-ebba-457a-b38b-9a17762f6141"), "+1684", "AS", true, "American Samoa" },
                    { new Guid("c6d869a6-9592-465c-bb33-3635511aae8e"), "+376", "AD", true, "Andorra" },
                    { new Guid("d42a610c-d6a2-4376-b8fa-e0cc24998d96"), "+244", "AO", true, "Angola" },
                    { new Guid("cdede895-15cf-4b19-a518-8da0211f76b7"), "+1264", "AI", true, "Anguilla" },
                    { new Guid("a228d247-1f29-487b-8c88-a7af387391c6"), "+672", "AQ", true, "Antarctica" },
                    { new Guid("874ef0b2-ca3e-41ef-af1b-2aea2c0a5769"), "+1268", "AG", true, "Antigua and Barbuda" },
                    { new Guid("ca39433d-6984-4567-a717-3fc948ea8d52"), "+54", "AR", true, "Argentina" },
                    { new Guid("6eeb4317-ea6c-493d-9af0-d1976d7100de"), "+374", "AM", true, "Armenia" },
                    { new Guid("c64c69c8-9060-4251-852b-1f40c2df9124"), "+297", "AW", true, "Aruba" },
                    { new Guid("714c382f-db01-4fca-bdfe-f0a7b244236f"), "+61", "AU", true, "Australia" },
                    { new Guid("ed266508-e26e-4708-8d21-23154619c3fa"), "+357", "CY", true, "Cyprus" },
                    { new Guid("c75b1cfd-ebe2-4255-a6b6-28643f872152"), "+43", "AT", true, "Austria" },
                    { new Guid("34d69641-9b8b-4c79-9e14-7ad3f49a7314"), "+1242", "BS", true, "Bahamas" },
                    { new Guid("477d8fbc-e80a-489e-aba2-736293a699fb"), "+973", "BH", true, "Bahrain" },
                    { new Guid("1cc45f40-784c-43b2-9b88-70d8b3cec6c3"), "+880", "BD", true, "Bangladesh" },
                    { new Guid("ab43ea54-d4b6-4b56-bf9e-91f73f8516ea"), "+1246", "BB", true, "Barbados" },
                    { new Guid("a380394e-7990-4864-bca2-2832527192d6"), "+375", "BY", true, "Belarus" },
                    { new Guid("845bda9a-97c2-48ca-bb31-8f25ceb2bcab"), "+32", "BE", true, "Belgium" },
                    { new Guid("c978ac1e-7cfe-412b-a231-3195e0b31279"), "+501", "BZ", true, "Belize" },
                    { new Guid("54372766-53d0-4c1c-9784-71e056d9c7d9"), "+229", "BJ", true, "Benin" },
                    { new Guid("725a2cd5-cd9d-4f5f-9abc-e340f0b5e07a"), "+1441", "BM", true, "Bermuda" },
                    { new Guid("6c99985d-4372-4d21-97c4-822863b28cf9"), "+975", "BT", true, "Bhutan" },
                    { new Guid("5c0d4d2b-9905-4485-9fc0-d8858ab2d3a8"), "+591", "BO", true, "Bolivia (Plurinational State of)" },
                    { new Guid("290a4868-b537-41bf-b514-b36ddde77e32"), "+5997", "BQ", true, "Bonaire, Sint Eustatius and Saba" },
                    { new Guid("529b34dd-12f1-4b52-9270-bd122ea8cc5f"), "+387", "BA", true, "Bosnia and Herzegovina" },
                    { new Guid("dc9ffe90-bb21-4e89-aea2-267ce1d029fd"), "+994", "AZ", true, "Azerbaijan" },
                    { new Guid("77c07dae-8595-4c6a-870f-02d6461dd202"), "+260", "ZM", true, "Zambia" },
                    { new Guid("b89fdca4-86a7-487a-b647-d2defee26a13"), "+420", "CZ", true, "Czech Republic" },
                    { new Guid("ee76f5bd-7d77-440f-9aaf-186af4e13ba5"), "+253", "DJ", true, "Djibouti" },
                    { new Guid("4f0eb234-b1f0-4410-846c-6bb4bab31519"), "+592", "GY", true, "Guyana" },
                    { new Guid("7e9acba5-29d1-400c-b198-3d235bc0c9dc"), "+509", "HT", true, "Haiti" },
                    { new Guid("d9cf3244-5eb0-4f47-ae9c-e48b948ad068"), "+", "HM", true, "Heard Island and McDonald Islands" },
                    { new Guid("818d2dc0-a89a-4c46-85a9-4f363e084187"), "+379", "VA", true, "Holy See" },
                    { new Guid("bd2c6e92-2765-4dc3-b79e-8d7c0ac6fdd4"), "+504", "HN", true, "Honduras" },
                    { new Guid("ebf23b09-1151-4855-a3f6-e9fd51fef715"), "+852", "HK", true, "Hong Kong" },
                    { new Guid("78eb9514-06e7-493e-a495-f84c531a7791"), "+36", "HU", true, "Hungary" },
                    { new Guid("f202c2af-1dd0-4882-99f0-082cb5776b2a"), "+354", "IS", true, "Iceland" },
                    { new Guid("1c6c52d5-c467-47d4-a18a-84b367ba4311"), "+91", "IN", true, "India" },
                    { new Guid("88b339cf-a9c9-4e44-ab05-5c6e3cadcb6d"), "+62", "id", true, "Indonesia" },
                    { new Guid("fce81138-2079-4bd2-ac13-e9e08e486232"), "+225", "CI", true, "Côte d'Ivoire" },
                    { new Guid("e2a89a18-6f76-4318-a87d-6e1eb5d913d1"), "+98", "IR", true, "Iran (Islamic Republic of)" },
                    { new Guid("ab0bfe63-8589-412a-afd5-f3e105d20568"), "+964", "IQ", true, "Iraq" },
                    { new Guid("5a547753-2720-4ef8-9fd7-ffadd1036c72"), "+353", "IE", true, "Ireland" },
                    { new Guid("cdafe458-b575-4207-823a-8c28e6630c70"), "+44", "IM", true, "Isle of Man" },
                    { new Guid("c9c252be-8cfe-4f34-ba7d-3c8b11b551c7"), "+972", "IL", true, "Israel" },
                    { new Guid("d9a2e10d-8735-405e-9ea1-a00a7784dd56"), "+39", "it", true, "Italy" },
                    { new Guid("e8407507-47db-4672-89f0-6052fc97ac5b"), "+1876", "JM", true, "Jamaica" },
                    { new Guid("cf2b6264-a1fe-43a2-b9d0-f92f41a62da8"), "+81", "JP", true, "Japan" },
                    { new Guid("9343700a-a38e-44a9-b62f-6a069a17fdb6"), "+44", "JE", true, "Jersey" },
                    { new Guid("dacf22ad-417b-436f-a73f-59a47b2324b4"), "+962", "JO", true, "Jordan" },
                    { new Guid("ba3775f7-cb5c-4437-b1f2-ca7156560e53"), "+76", "KZ", true, "Kazakhstan" },
                    { new Guid("66e5ac2b-c3ee-49c3-a25a-437b3d918e91"), "+254", "KE", true, "Kenya" },
                    { new Guid("098ad535-c846-4473-8a6d-a89f1f762e93"), "+686", "KI", true, "Kiribati" },
                    { new Guid("92a086ad-64cc-4209-a1a7-c2002c1dfe69"), "+965", "KW", true, "Kuwait" },
                    { new Guid("2cdba5e4-4235-4c15-8eff-9dbbde91c8cc"), "+996", "KG", true, "Kyrgyzstan" },
                    { new Guid("a7e1fca7-dcc0-4873-9d1f-c170b5bf25a9"), "+856", "LA", true, "Lao People's Democratic Republic" },
                    { new Guid("abddcd95-2dda-48e1-9e41-0503df5d489c"), "+245", "GW", true, "Guinea-Bissau" },
                    { new Guid("670cba88-5a11-42b9-9377-f49805633bf4"), "+224", "GN", true, "Guinea" },
                    { new Guid("c0fb1455-53a6-44e6-88dd-1809082063c7"), "+44", "GG", true, "Guernsey" },
                    { new Guid("61662609-003a-4097-95f8-6598bf26a4ef"), "+502", "GT", true, "Guatemala" },
                    { new Guid("4f720a85-1987-4d83-8739-b71595daaa8b"), "+1767", "DM", true, "Dominica" },
                    { new Guid("168ca0f0-7e9f-499f-96e8-4131fc0f70a6"), "+1809", "DO", true, "Dominican Republic" },
                    { new Guid("8a54bca0-807d-4cdf-8047-d317433757f5"), "+593", "EC", true, "Ecuador" },
                    { new Guid("41a606dd-4c52-438d-a9ec-df14591cbf3e"), "+20", "EG", true, "Egypt" },
                    { new Guid("11b4d693-dd81-4b9b-8c80-50a2fae110d3"), "+503", "SV", true, "El Salvador" },
                    { new Guid("fb4d2c5a-5f93-4130-a03a-6885d2ff906b"), "+240", "GQ", true, "Equatorial Guinea" },
                    { new Guid("0d41eff4-8d55-43e1-b94d-39631d7bfb54"), "+291", "ER", true, "Eritrea" },
                    { new Guid("d9bc2c41-5b53-473c-869c-6cab75c50a59"), "+372", "EE", true, "Estonia" },
                    { new Guid("b9fdfe8b-b370-4ffe-9160-d5a76af13a39"), "+251", "ET", true, "Ethiopia" },
                    { new Guid("c57a4fb1-b16e-4371-8553-e120064084f9"), "+500", "FK", true, "Falkland Islands (Malvinas)" },
                    { new Guid("495cc6a5-0016-44e1-8004-e7fbfd41a630"), "+298", "FO", true, "Faroe Islands" },
                    { new Guid("6e2cb01a-d6c3-4ca2-8b13-48b3367735d2"), "+679", "FJ", true, "Fiji" },
                    { new Guid("b4dde644-e38a-48ce-883e-0a9d8dd65758"), "+358", "FI", true, "Finland" },
                    { new Guid("f1ac9aeb-e891-4315-ac5e-5e9beebc3305"), "+45", "DK", true, "Denmark" },
                    { new Guid("cc7464a1-8300-450d-bd12-9e93c5307a09"), "+33", "fr", true, "France" },
                    { new Guid("95a16c33-9300-4336-bc0b-c2e02220885e"), "+689", "PF", true, "French Polynesia" },
                    { new Guid("fce57936-adc2-4a55-90be-b4c3476fd23e"), "+", "TF", true, "French Southern Territories" },
                    { new Guid("8c772dab-d696-403e-96ba-5d3c3284dfc1"), "+241", "GA", true, "Gabon" },
                    { new Guid("068f8b83-66a2-47ba-8285-7e815cf9c677"), "+220", "GM", true, "Gambia" },
                    { new Guid("fa1f6ca9-2c38-4ab6-b361-1af88e919ae5"), "+995", "GE", true, "Georgia" },
                    { new Guid("b04eb0d3-e4f6-4803-8bac-913ed58cd7d1"), "+49", "de", true, "Germany" },
                    { new Guid("cfe3c5b5-d629-4887-83f9-ea9e3ea32ae8"), "+233", "GH", true, "Ghana" },
                    { new Guid("0a25ab32-695f-4ecb-a47c-e0da577d36ee"), "+350", "GI", true, "Gibraltar" },
                    { new Guid("b7b1727a-e52f-45af-8a06-8337320eb960"), "+30", "GR", true, "Greece" },
                    { new Guid("8a8d21ac-da3d-47f0-8049-84fbbaeee508"), "+299", "GL", true, "Greenland" },
                    { new Guid("27e8ba9e-58fc-48a1-8e92-f3f8bc846557"), "+1473", "GD", true, "Grenada" },
                    { new Guid("4f3cbce7-8ab8-44e5-a58f-4b4a7da09f43"), "+590", "GP", true, "Guadeloupe" },
                    { new Guid("fbb1feed-b0d0-4a50-9e88-bb541fc56537"), "+1671", "GU", true, "Guam" },
                    { new Guid("c0eb726b-84d4-442d-bb98-4eb0f9d628b6"), "+594", "GF", true, "French Guiana" },
                    { new Guid("6c234af4-d025-4815-924e-1d98609128ae"), "+263", "ZW", true, "Zimbabwe" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_PictureId",
                schema: "aiala",
                table: "Tasks",
                column: "PictureId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledTasks_PictureId",
                schema: "aiala",
                table: "ScheduledTasks",
                column: "PictureId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledPlace_PictureId",
                schema: "aiala",
                table: "ScheduledPlace",
                column: "PictureId");

            migrationBuilder.CreateIndex(
                name: "IX_Places_PictureId",
                schema: "aiala",
                table: "Places",
                column: "PictureId");

            migrationBuilder.CreateIndex(
                name: "IX_AiPictureMetadatas_PictureId",
                schema: "aiala",
                table: "AiPictureMetadatas",
                column: "PictureId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AiPictureTags_PictureId",
                schema: "aiala",
                table: "AiPictureTags",
                column: "PictureId");

            migrationBuilder.Sql("UPDATE aiala.Places SET PictureId = NULL");
            migrationBuilder.AddForeignKey(
                name: "FK_Places_Pictures_PictureId",
                schema: "aiala",
                table: "Places",
                column: "PictureId",
                principalSchema: "aiala",
                principalTable: "Pictures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.Sql("UPDATE aiala.ScheduledPlace SET PictureId = NULL");
            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledPlace_Pictures_PictureId",
                schema: "aiala",
                table: "ScheduledPlace",
                column: "PictureId",
                principalSchema: "aiala",
                principalTable: "Pictures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.Sql("UPDATE aiala.ScheduledTasks SET PictureId = NULL");
            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledTasks_Pictures_PictureId",
                schema: "aiala",
                table: "ScheduledTasks",
                column: "PictureId",
                principalSchema: "aiala",
                principalTable: "Pictures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.Sql("UPDATE aiala.Tasks SET PictureId = NULL");
            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Pictures_PictureId",
                schema: "aiala",
                table: "Tasks",
                column: "PictureId",
                principalSchema: "aiala",
                principalTable: "Pictures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Places_Pictures_PictureId",
                schema: "aiala",
                table: "Places");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledPlace_Pictures_PictureId",
                schema: "aiala",
                table: "ScheduledPlace");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledTasks_Pictures_PictureId",
                schema: "aiala",
                table: "ScheduledTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Pictures_PictureId",
                schema: "aiala",
                table: "Tasks");

            migrationBuilder.DropTable(
                name: "AiPictureTags",
                schema: "aiala");

            migrationBuilder.DropTable(
                name: "AiPictureMetadatas",
                schema: "aiala");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_PictureId",
                schema: "aiala",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_ScheduledTasks_PictureId",
                schema: "aiala",
                table: "ScheduledTasks");

            migrationBuilder.DropIndex(
                name: "IX_ScheduledPlace_PictureId",
                schema: "aiala",
                table: "ScheduledPlace");

            migrationBuilder.DropIndex(
                name: "IX_Places_PictureId",
                schema: "aiala",
                table: "Places");

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("02778799-6b45-47be-923f-7a1cd2e287a2"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("04549a74-b458-4bc6-94de-d339d5ed12e1"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("0516741a-702a-4c5a-9b7e-92f21356b336"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("05e5ab87-dc04-404e-925d-ac6c90d82427"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("068f8b83-66a2-47ba-8285-7e815cf9c677"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("08051e14-d13e-4e9e-90a6-0f3c4fc7b6d8"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("098ad535-c846-4473-8a6d-a89f1f762e93"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("0a1fadde-e103-40f3-94f8-ae58e70ee91d"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("0a25ab32-695f-4ecb-a47c-e0da577d36ee"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("0b153c4a-481f-4cbb-a488-d8e71ea05ff3"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("0b4f312f-3456-4cc9-ba85-acb48b9e7e9e"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("0bf9b7ac-0e84-41bd-8e97-b81352e451f2"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("0d41eff4-8d55-43e1-b94d-39631d7bfb54"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("0da60206-a3f5-4a18-a8b6-630e12719c82"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("0e14972b-4f7c-4d6d-938b-ca6360b0872b"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("0fe76a29-bd01-4578-b770-f1f51dbf7172"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("11b4d693-dd81-4b9b-8c80-50a2fae110d3"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("12d54c0e-6444-43fb-b27e-d82e319480e3"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("142dac4e-e495-4c29-aaac-5aa6c2b2f630"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("158eb17f-f08d-42cc-95a4-057f7133ae51"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("15d6f9ea-1026-4ad3-88f8-f3e8f1eb6f6e"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("168ca0f0-7e9f-499f-96e8-4131fc0f70a6"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("1855fe95-33ad-4a68-bcc3-c57ed85ce329"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("1b6384c7-92a9-42b3-b996-c0b134ab1170"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("1c6c52d5-c467-47d4-a18a-84b367ba4311"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("1cc45f40-784c-43b2-9b88-70d8b3cec6c3"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("1d27b72f-31d6-4568-86cc-7b6ae7e206a3"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("1eb5343a-b090-4efc-a684-ba5d5a613516"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("20b93646-a854-4cbc-9e45-f32cfe536f00"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("22936236-016e-4c06-aff9-d33d56c7c901"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("23440164-15e7-4aa2-ba50-6699b81b31ff"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("234e687b-9acf-4268-9d64-1b622f43e673"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("27e8ba9e-58fc-48a1-8e92-f3f8bc846557"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("290a4868-b537-41bf-b514-b36ddde77e32"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("2a032347-4030-4119-babc-95e1da3e8e16"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("2a7a8f6b-dda3-4799-8491-f4d03825899a"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("2a7a9ede-fcf3-4455-9cd2-daaf2f07851d"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("2c691e17-979b-4192-a106-3ccc4da056a6"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("2cdba5e4-4235-4c15-8eff-9dbbde91c8cc"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("2e38cb7e-7dc3-40a7-a947-533fd1134105"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("302ecafb-2dcb-466c-ab01-cf12d7b1544e"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("31387bd8-b5d7-41c0-9807-d39d3a4453dc"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("34d69641-9b8b-4c79-9e14-7ad3f49a7314"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("363ae089-3a30-478d-b49f-5c7b9bdafe50"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("36a1b84a-849d-4fb1-ba49-8b8d85011554"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("39035db4-10a0-4070-9fa6-b703a9a79199"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("39dcf65d-b256-4c48-9ffe-2cfd92d9adb1"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("3abddad7-fa8f-456b-a590-33c5083b98dd"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("3fdd5f8d-f594-41d3-aeb6-ffabcfe62f36"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("3fea9a2d-c0ef-40a3-8128-c589e95c3537"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("41a606dd-4c52-438d-a9ec-df14591cbf3e"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("431af217-da9c-49c0-862d-346c3f154807"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("4474dd19-7cff-4df2-9de6-f1f52b9aa94c"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("44e996a7-fb2c-41bf-917f-02f3dd79f639"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("453ffcfa-6e29-41db-99a1-72d0067abf30"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("477d8fbc-e80a-489e-aba2-736293a699fb"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("47b942da-f582-4a85-9347-edf92e14ea86"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("495cc6a5-0016-44e1-8004-e7fbfd41a630"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("4cdcb59b-7021-4c86-8fa2-87b807460ad9"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("4f0eb234-b1f0-4410-846c-6bb4bab31519"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("4f3cbce7-8ab8-44e5-a58f-4b4a7da09f43"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("4f720a85-1987-4d83-8739-b71595daaa8b"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("50edd070-117c-4b0a-ad32-97607b3b4fb5"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("5198fbd1-17c2-4130-b3ba-377c37c893af"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("529b34dd-12f1-4b52-9270-bd122ea8cc5f"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("54372766-53d0-4c1c-9784-71e056d9c7d9"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("54d6b82d-49da-44a5-8387-44eb555c8d44"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("56aca540-4ae2-43cf-bdff-72ea9b868780"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("583cb448-7e2d-4c30-9835-343bbf977e9b"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("5a547753-2720-4ef8-9fd7-ffadd1036c72"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("5ad56577-7098-4515-b010-06324670a069"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("5c0d4d2b-9905-4485-9fc0-d8858ab2d3a8"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("5c62dbd2-c02e-4ae9-8b14-031c6c874659"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("6022bae6-98fd-4aa6-8900-b747f6c35044"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("603218e7-8773-44ef-a1de-34b7d5d7b21e"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("61662609-003a-4097-95f8-6598bf26a4ef"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("64197b44-3373-4b0f-ada8-64a0066029ff"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("64684184-0951-47e4-bac3-282314ccc2bd"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("668e3ca5-8bfb-4f8c-8c98-207e3889f0fe"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("66e5ac2b-c3ee-49c3-a25a-437b3d918e91"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("670cba88-5a11-42b9-9377-f49805633bf4"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("68f56a68-74d2-4ff9-99cb-a946d96edd1b"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("6a1bb25b-a7c8-4a72-bbfa-e3e7f0a7112b"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("6aa8a2e4-5b28-4fc0-a1ad-e80ad2426ede"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("6aeaa091-8c4b-47a9-94f0-6e2b9636bf72"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("6c234af4-d025-4815-924e-1d98609128ae"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("6c99985d-4372-4d21-97c4-822863b28cf9"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("6e2cb01a-d6c3-4ca2-8b13-48b3367735d2"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("6eeb4317-ea6c-493d-9af0-d1976d7100de"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("714c382f-db01-4fca-bdfe-f0a7b244236f"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("725a2cd5-cd9d-4f5f-9abc-e340f0b5e07a"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("7401ab85-99a8-40bd-b5b0-c4d94d25aa69"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("743de50e-6fa1-4272-97e1-06bc862fe505"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("7659bcc4-41cf-4846-bab8-2bae6f938359"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("76819034-2d0b-4296-bb93-4c61927f9d8d"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("77c07dae-8595-4c6a-870f-02d6461dd202"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("78eb9514-06e7-493e-a495-f84c531a7791"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("7945287d-cd87-4153-a98d-341a50f701bf"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("7973277f-00e4-4518-be78-a811f244bed5"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("7a551b34-cb89-40bd-820e-7bf0c5603536"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("7b1130ba-25c5-4886-8683-9955f720c67e"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("7cf19a97-a4f6-4a07-8dee-8175b1c3cd7d"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("7e9acba5-29d1-400c-b198-3d235bc0c9dc"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("7f8afd92-61bd-477c-bf78-12d62b3e5cb5"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("7fbe832c-04f2-4f09-932c-af642d295032"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("80ddc377-d6d3-40d0-aa85-b17907310d2b"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("818d2dc0-a89a-4c46-85a9-4f363e084187"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("81a28038-c55d-4eca-8e68-668b2864fd98"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("8216f3c7-2a58-42e8-acd4-308021850354"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("8328c807-7bb3-4c7e-82d8-d72b0c148204"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("83e157d8-1021-4d51-9a4a-2d627e769456"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("845bda9a-97c2-48ca-bb31-8f25ceb2bcab"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("871803b6-865a-4aee-8e4d-95189da19c50"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("874ef0b2-ca3e-41ef-af1b-2aea2c0a5769"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("88b339cf-a9c9-4e44-ab05-5c6e3cadcb6d"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("8a54bca0-807d-4cdf-8047-d317433757f5"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("8a8d21ac-da3d-47f0-8049-84fbbaeee508"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("8a951ed8-ca8c-4e8a-8232-d728d8a8a3e5"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("8c772dab-d696-403e-96ba-5d3c3284dfc1"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("8cce699e-b033-4b9b-902e-6483da7ca19a"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("8e6e14e7-d429-4837-bb2b-e84f6cc3299a"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("8f532a74-0192-43ce-88e2-b57335b1daaf"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("900794cd-2b60-41da-97ae-c7bc082f9cf9"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("91296619-d79b-4f9f-b0b1-5356f17bf26e"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("92414cfd-4ba3-469d-b951-3ff06eecffdc"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("92a086ad-64cc-4209-a1a7-c2002c1dfe69"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("9343700a-a38e-44a9-b62f-6a069a17fdb6"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("95a16c33-9300-4336-bc0b-c2e02220885e"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("9651830d-20e4-4222-aa36-1b596500670d"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("994c8be2-8fa3-43ee-a725-41895002f499"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("9c22df3d-bbbf-4a8a-a0ef-b1720bc43463"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("9d19efbd-70e8-4817-afbb-fb735ff78ad7"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("9e974584-f073-415c-b535-9bf6f8f83fbc"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("9f53e4c4-bc1e-4d26-b0a0-d1d589f9e443"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("a0abe09e-395c-48d3-bef7-3f9282b1c60d"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("a2141dc6-5c4b-400f-bdb1-211ac5081954"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("a228d247-1f29-487b-8c88-a7af387391c6"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("a349e604-c2d1-403f-82ec-01c41c26825e"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("a380394e-7990-4864-bca2-2832527192d6"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("a53ee786-5790-445b-95cc-cdf563cb599e"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("a7e1fca7-dcc0-4873-9d1f-c170b5bf25a9"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("a9501973-f190-471f-b299-acd6085ccfbc"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("aa85fc2b-e3a5-4849-b869-724d61bc383e"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("ab0bfe63-8589-412a-afd5-f3e105d20568"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("ab43ea54-d4b6-4b56-bf9e-91f73f8516ea"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("abddcd95-2dda-48e1-9e41-0503df5d489c"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("ac050c4f-a0bc-4e0d-88ca-2853b9438d85"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("ac714d22-4b36-4776-b842-728d72319087"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("ae7b0ee4-e00f-436d-a9cc-55eb493a392f"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("aef0117a-7970-4a60-9fc1-279c8c8b6eb6"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("afc0c551-cec0-4999-a9fe-7f92710785ac"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("b04eb0d3-e4f6-4803-8bac-913ed58cd7d1"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("b07d1ada-7a09-484e-aa77-97ce1b769ac7"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("b21c14d5-09e3-4f22-952e-f739db8b2dc0"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("b4dde644-e38a-48ce-883e-0a9d8dd65758"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("b54fac30-8dbd-4c34-921b-ecb780d45e05"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("b7b1727a-e52f-45af-8a06-8337320eb960"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("b89fdca4-86a7-487a-b647-d2defee26a13"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("b9fdfe8b-b370-4ffe-9160-d5a76af13a39"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("ba3775f7-cb5c-4437-b1f2-ca7156560e53"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("bb691dd6-b2e1-4a61-8329-58f233d66d3a"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("bba868d5-0661-4421-8141-b13297e96218"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("bc198476-8c8b-433e-80ad-c78d93b65758"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("bd2c6e92-2765-4dc3-b79e-8d7c0ac6fdd4"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("bdbdeb23-9199-4a6c-b0f5-33014965fe40"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("be94fb67-a08c-419f-a9bc-dda56d4b5096"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("bf5f1f0f-9b12-41a4-a1aa-438c6a3af860"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c00c5c0b-c6be-4d27-b4db-302443d57439"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c046fa0d-300b-4d38-b811-34aebae66b83"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c052a003-3341-442e-825b-fcbc3c208498"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0eb726b-84d4-442d-bb98-4eb0f9d628b6"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0fb1455-53a6-44e6-88dd-1809082063c7"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c2195d27-4ca3-46f9-b289-8250e32dd019"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c2473d84-7c6b-4b32-a624-77419bef5104"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c355b129-8496-4f66-b39b-a05b1e140f24"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c3cdf8e2-45c4-4f11-a91d-fa1a43bcd9f9"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c41c146e-66c6-4b79-83bc-d18117231f31"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c4fb5e61-c37f-4e5f-b70b-65c7ff3a24b8"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c50719ef-6ccb-41d7-9244-5c82dde5e804"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c57a4fb1-b16e-4371-8553-e120064084f9"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c5f5f3ee-f4e1-4569-8e92-58a2b4a569cb"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c64c69c8-9060-4251-852b-1f40c2df9124"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c6b48dac-ceaf-45f6-a71c-70dcb28e3dd3"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c6d869a6-9592-465c-bb33-3635511aae8e"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c7524e4a-6065-4fa6-a33b-215ae8f87e86"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c75b1cfd-ebe2-4255-a6b6-28643f872152"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c895cb3b-0be9-44e5-8bd9-3c5ddbeac938"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c8ac2c56-36fd-40f2-9ea9-6974f2387a0e"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c92e3b7f-e7aa-4b23-bd38-8ef653b7f7a2"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c978ac1e-7cfe-412b-a231-3195e0b31279"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c9c252be-8cfe-4f34-ba7d-3c8b11b551c7"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("ca39433d-6984-4567-a717-3fc948ea8d52"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("cb7d0d8e-7612-44b6-a1d6-9ac33762a823"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("cbb6a6af-e1e0-4d6d-a908-58b8f130576e"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("cc7464a1-8300-450d-bd12-9e93c5307a09"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("cc76e0b5-40d7-40cc-ac59-c6dcd1b29cf0"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("cdafe458-b575-4207-823a-8c28e6630c70"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("cdede895-15cf-4b19-a518-8da0211f76b7"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("ce56bc91-b97f-4f14-8c02-7e4ec2c6b1be"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("cf2b6264-a1fe-43a2-b9d0-f92f41a62da8"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("cfe3c5b5-d629-4887-83f9-ea9e3ea32ae8"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("d2fc318d-ef9a-49d3-8db3-1264b8415728"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("d42a610c-d6a2-4376-b8fa-e0cc24998d96"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("d55e3ff4-05e5-41f3-9e63-7a330f920950"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("d6a33871-b015-4015-ac21-9a95ae8c89e5"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("d6ecbf46-a0a5-4e61-89f9-c9acd0782532"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("d83ca70f-d185-4c87-b82a-6cba91de7531"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("d8b3c897-69e1-4bd0-aecf-668b0dadffc7"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("d9a2e10d-8735-405e-9ea1-a00a7784dd56"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("d9bc2c41-5b53-473c-869c-6cab75c50a59"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("d9cf3244-5eb0-4f47-ae9c-e48b948ad068"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("da4bd7d8-23a5-4a40-804f-e9a67cfa93a6"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("dacf22ad-417b-436f-a73f-59a47b2324b4"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("db9524e7-418a-47ca-a207-982af4c59666"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("dc48c0ec-8c3b-4d06-9283-935fa5453730"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("dc9ffe90-bb21-4e89-aea2-267ce1d029fd"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("dd4b36b5-4499-48c7-ae89-b8f18c4b6fb9"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("dda9bd73-0cb2-423d-adcc-ee0d393b22b5"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("e0be6610-da6e-4d3d-8699-8825fb4ee337"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("e2a89a18-6f76-4318-a87d-6e1eb5d913d1"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("e445207e-d79a-40b1-a108-3f6f8a37d503"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("e6e85a31-ae53-43ee-b21a-f6ed0538b020"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("e796d639-b073-4de5-b761-907ea7fc4ffc"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("e8407507-47db-4672-89f0-6052fc97ac5b"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("ebf23b09-1151-4855-a3f6-e9fd51fef715"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("ecd1a960-7846-4599-b78c-ae0bbfbf8f54"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("ed266508-e26e-4708-8d21-23154619c3fa"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("ede02c3f-9b24-4d85-a0c7-021e5386c861"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("ee76f5bd-7d77-440f-9aaf-186af4e13ba5"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("efa9f8fb-92f8-46da-8171-b2947811b8cf"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("f0b72653-ebba-457a-b38b-9a17762f6141"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("f1ac9aeb-e891-4315-ac5e-5e9beebc3305"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("f1e7967a-0237-4ec2-95bc-f92fb9cf5537"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("f202c2af-1dd0-4882-99f0-082cb5776b2a"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("f28a2a31-b33e-410a-8418-5f84240a37d2"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("f2c63602-3db9-401f-a8bc-e352359dd77d"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("f2fde10e-0e24-4a72-9f1f-2c248bae340c"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("f30caf70-5235-4f4d-b300-c63efa33b1b1"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("f59c01d4-367d-4773-8c4c-2b47e4f2e3a0"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("f7dcbb8a-90d6-4008-89f9-fa8cd241d66f"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("f7fd2d44-31cc-4b71-8afd-cedf532cc605"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("f83ba14a-2758-45ef-93df-10170b0759c3"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("fa1f6ca9-2c38-4ab6-b361-1af88e919ae5"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("fb4d2c5a-5f93-4130-a03a-6885d2ff906b"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("fbb1feed-b0d0-4a50-9e88-bb541fc56537"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("fcb3d8e1-2a9c-470f-b7c5-15dea06477c9"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("fcdc7d01-ea17-4ac8-b712-f778ee2de9f2"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("fce57936-adc2-4a55-90be-b4c3476fd23e"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("fce81138-2079-4bd2-ac13-e9e08e486232"));

            migrationBuilder.DeleteData(
                schema: "directory",
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("feb9d6d5-2327-4065-9cbc-a80d0b0ebc89"));

            migrationBuilder.DropColumn(
                name: "CallingCode",
                schema: "directory",
                table: "Countries");

            migrationBuilder.AlterColumn<Guid>(
                name: "PictureId",
                schema: "aiala",
                table: "ScheduledPlace",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "PictureId",
                schema: "aiala",
                table: "Places",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "aiala",
                table: "Pictures",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "DescriptionConfidence",
                schema: "aiala",
                table: "Pictures",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<bool>(
                name: "HasHumanConfidence",
                schema: "aiala",
                table: "Pictures",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "PicturesTags",
                schema: "aiala",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Confidence = table.Column<float>(nullable: false),
                    HasHumanConfidence = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    PictureId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PicturesTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PicturesTags_Pictures_PictureId",
                        column: x => x.PictureId,
                        principalSchema: "aiala",
                        principalTable: "Pictures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PicturesTags_PictureId",
                schema: "aiala",
                table: "PicturesTags",
                column: "PictureId");
        }
    }
}
