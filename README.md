# BarberSalonPrototype

A complete ASP.NET Core MVC wireframe prototype for a Barbershop Salon website with comprehensive features for staff management, services, booking, gallery, and more.

## Features

### Complete Website Features
- **Home Page**: Hero banner with CTA, welcome text, and quick links
- **Services Page**: List of salon services with prices and booking links
- **Booking Page**: Online appointment booking form
- **Gallery Page**: Portfolio of work with modal image viewer
- **About Us Page**: Salon history, mission, team overview, and operating hours
- **Contact Page**: Contact form, address, and Google Maps integration
- **Staff Management**: Complete team profiles with galleries and social media links

### Key Components

#### Models
- `StaffMember`: Contains staff information including name, role, description, specializations, social media links, and gallery
- `GalleryImage`: Individual images with captions for staff portfolios
- `ErrorViewModel`: Standard error handling model

#### Controllers
- `HomeController`: Home page with hero banner and quick links
- `ServicesController`: Displays salon services with prices
- `BookingController`: Handles appointment booking form
- `GalleryController`: Shows portfolio of work with modal viewer
- `AboutController`: Salon information, history, and operating hours
- `ContactController`: Contact form and location information
- `StaffController`: 
  - `Index()`: Displays all staff members in a grid layout
  - `Details(int id)`: Shows individual staff member profile with gallery

#### Views
- `Views/Home/Index.cshtml`: Hero banner, welcome text, and quick links section
- `Views/Services/Index.cshtml`: Service cards with prices and booking buttons
- `Views/Booking/Index.cshtml`: Appointment booking form with all required fields
- `Views/Gallery/Index.cshtml`: Image grid with modal viewer
- `Views/About/Index.cshtml`: Salon history, mission, team overview, and hours
- `Views/Contact/Index.cshtml`: Contact form, address, and Google Maps
- `Views/Staff/Index.cshtml`: Grid layout of staff cards with hover effects
- `Views/Staff/Details.cshtml`: Individual profile page with carousel gallery and social media links
- `Views/Shared/_Layout.cshtml`: Main layout with complete navigation menu

### Sample Data
The application includes 5 sample staff members:
1. **Michael Rodriguez** - Master Barber
2. **Sarah Johnson** - Hair Stylist  
3. **David Chen** - Barber
4. **Emily Martinez** - Nail Technician
5. **Alex Thompson** - Skin Care Specialist

Each staff member has:
- Professional profile image
- Detailed description
- List of specializations
- Social media links (Instagram, Facebook, TikTok)
- Gallery of work samples

## Technology Stack

- **ASP.NET Core 8.0** - Web framework
- **MVC Pattern** - Architecture
- **Bootstrap 5** - UI framework
- **Font Awesome** - Icons
- **jQuery** - JavaScript library

## Project Structure

```
BarberSalonPrototype/
├── Controllers/
│   ├── HomeController.cs
│   └── StaffController.cs
├── Models/
│   ├── ErrorViewModel.cs
│   ├── GalleryImage.cs
│   └── StaffMember.cs
├── Views/
│   ├── Home/
│   │   ├── Index.cshtml
│   │   └── Privacy.cshtml
│   ├── Staff/
│   │   ├── Index.cshtml
│   │   └── Details.cshtml
│   └── Shared/
│       ├── _Layout.cshtml
│       └── Error.cshtml
├── wwwroot/
│   ├── css/
│   │   └── site.css
│   └── js/
│       └── site.js
├── Program.cs
├── appsettings.json
└── BarberSalonPrototype.csproj
```

## Getting Started

### Prerequisites
- .NET 8.0 SDK or later
- Visual Studio 2022 or VS Code

### Installation
1. Clone the repository
2. Navigate to the project directory
3. Run the application:
   ```bash
   dotnet run
   ```
4. Open your browser and navigate to `https://localhost:5001` or `http://localhost:5000`

### Navigation
- **Home**: Welcome page with link to team
- **Our Team**: Staff listing page (`/Staff/Index`)
- **Individual Profiles**: Click "View Profile" on any staff card to see details

## Features in Detail

### Staff Index Page
- Responsive grid layout (1 column on mobile, 2 on tablet, 3 on desktop)
- Staff cards with profile images, names, roles, and truncated descriptions
- Specialization badges (shows first 3, with "+X more" indicator)
- Hover effects and smooth transitions
- "View Profile" buttons linking to individual pages

### Staff Details Page
- Breadcrumb navigation
- Staff information card with full description
- All specializations displayed as badges
- Social media icons with links (Instagram, Facebook, TikTok)
- Bootstrap carousel for gallery images
- Thumbnail navigation below carousel
- Responsive design for mobile devices

### UI/UX Features
- Modern, clean design with Bootstrap 5
- Smooth hover animations
- Responsive layout for all screen sizes
- Font Awesome icons for social media
- Professional color scheme
- Card-based layout with shadows

## Customization

### Adding New Staff Members
1. Edit `StaffController.cs`
2. Add new `StaffMember` objects to the `_staffMembers` list
3. Include profile image, description, specializations, and social media links
4. Add gallery images as needed

### Styling
- Modify `wwwroot/css/site.css` for custom styles
- Update `Views/Shared/_Layout.cshtml` for layout changes
- Customize Bootstrap classes in view files

### Images
- Place staff profile images in `wwwroot/images/staff/`
- Place gallery images in `wwwroot/images/gallery/`
- Update image paths in the controller data

## Future Enhancements

Potential improvements for the application:
- Database integration (Entity Framework)
- Admin panel for managing staff
- Image upload functionality
- Appointment booking system
- Customer reviews and ratings
- Service catalog integration
- Multi-language support

## License

This project is created for educational and demonstration purposes. 