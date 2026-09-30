# Ulan-Ude Beton

Vue 3 + Vite site for a gas concrete company.

## Setup

```bash
npm install
```

## Development

```bash
npm run dev
```

Local URL:

```text
http://localhost:5173
```

## CMS

The project uses Decap CMS as a lightweight Git-based admin panel. Editable
content lives in `src/content/site.json`, and the admin UI is available at:

```text
http://localhost:5173/admin/
```

For local editing, run the site and the CMS local backend in two terminals:

```bash
npm run dev
npm run cms
```

On production hosting, replace the `backend` section in
`public/admin/config.yml` with the selected Git backend settings, for example
Netlify Identity/Git Gateway or GitHub OAuth. The public site remains a static
Vite build, so it can be moved to almost any hosting that serves `dist/`.

## Fonts

The design uses local webfonts from Google Fonts:

```text
public/fonts/unbounded-cyrillic.woff2
public/fonts/unbounded-latin.woff2
public/fonts/manrope-cyrillic.woff2
public/fonts/manrope-latin.woff2
```

Headings use Unbounded, and the interface/body text uses Manrope.

## Production Build

```bash
npm run build
```

The production-ready static files are generated in `dist/`.
