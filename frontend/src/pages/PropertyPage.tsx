import PropertyPageMainSection from "@/components/properties/PropertyPageMainSection";
import PropertyPageSidebar from "@/components/properties/PropertyPageSidebar";
import { usePropertiesStore } from "@/stores/propertiesStore";
import { Property } from "@/types/property";
import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { toast } from "react-toastify";

export default function PropertyPage() {
  const { id } = useParams<{ id: string }>();
  const [property, setProperty] = useState<Property | undefined>();
  const { loading, getPropertyById } = usePropertiesStore();

  const navigate = useNavigate();

  useEffect(() => {
    const fetchProperty = async () => {
      if (!id) {
        toast.error("Invalid property entered!");
        navigate("/properties");
      }
      const fetchedProperty = await getPropertyById(id!);
      if (fetchedProperty) {
        setProperty(fetchedProperty);
      } else {
        toast.error("There was an error with that property");
        navigate("/properties");
      }
    };
    fetchProperty();
  }, [getPropertyById, id, navigate]);

  if (loading === true) {
    return "Loading...";
  }

  if (!property) {
    return "There was an error fetching the property";
  }

  return (
    <div className="mt-[200px] container mx-auto grid grid-cols-4 gap-10 lg:gap-20 mb-10 lg:mb-20 px-6">
      <PropertyPageMainSection property={property} />
      <PropertyPageSidebar property={property} />
    </div>
  );
}
