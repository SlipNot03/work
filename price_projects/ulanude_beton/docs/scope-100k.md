# Scope: corporate catalog site, 100 000 rub.

This document fixes the intended project scope for the current Vue/Vite site.

Main reference: https://gbsk.spb.ru/

## Included

- Responsive Vue 3 + Vite website.
- Main pages: home, catalog, about, delivery, where to buy, calculator, contacts.
- Header with top contact bar and primary buy action.
- Factory-style first screen with key benefits.
- News/promo cards for seasonal announcements and offers.
- Static product catalog with product cards and key specifications.
- Catalog filters by product category and density.
- Order table with product sizes, pallet data, density and price.
- Preliminary gas concrete calculator:
  - wall area;
  - outer wall volume;
  - partition volume;
  - volume;
  - pallets;
  - glue bags;
  - approximate material cost.
- Quick quote wizard for a commercial proposal draft.
- Contact form layout prepared for later backend/email integration.
- Yandex map embed with page scroll preserved.
- Basic commercial content blocks:
  - advantages;
  - workflow;
  - delivery terms;
  - delivery zones;
  - contact methods;
  - requisites notes.
- FAQ block for objections before order.
- Trust/document blocks for certificates and technical information.
- Production statistics and gas concrete benefits section.
- Production build via Vite.

## Not Included

- Admin panel.
- Online payment.
- Shopping cart.
- CRM integration.
- Automatic email/SMS delivery.
- Complex delivery calculation by routes or kilometers.
- Real inventory synchronization.
- Professional copywriting and SEO core research.
- Photo shoot and custom brand identity.

## Deployment

The project builds into static files:

```bash
npm run build
```

The `dist/` folder can be uploaded to a static hosting provider.
