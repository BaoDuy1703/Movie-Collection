# ===== Build stage =====
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# copy csproj trước để cache restore
COPY PRN232_PE_FA25_LeVoBaoDuy/*.csproj PRN232_PE_FA25_LeVoBaoDuy/
RUN dotnet restore PRN232_PE_FA25_LeVoBaoDuy/PRN232_PE_FA25_LeVoBaoDuy.csproj

# copy toàn bộ source và publish
COPY . .
RUN dotnet publish PRN232_PE_FA25_LeVoBaoDuy/PRN232_PE_FA25_LeVoBaoDuy.csproj -c Release -o /app/out

# ===== Runtime stage =====
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

# Render cấp biến $PORT → bind đúng địa chỉ
CMD ["sh","-c","dotnet PRN232_PE_FA25_LeVoBaoDuy.dll --urls=http://0.0.0.0:$PORT"]
