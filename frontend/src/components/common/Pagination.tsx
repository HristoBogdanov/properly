import { useNavigate } from "react-router-dom";
import { GrPrevious, GrNext } from "react-icons/gr";

type PaginationProps = {
  baseUrl: string;
  currentPage: number;
  perPage: number;
  totalPages: number;
};

export default function Pagination({
  baseUrl,
  currentPage,
  perPage,
  totalPages,
}: PaginationProps) {
  const navigate = useNavigate();

  if (totalPages < 2) return null;

  function navigateToPage(page: number) {
    navigate(
      `${baseUrl}${
        baseUrl === "/properties" ? "?" : "&"
      }page=${page}&perPage=${perPage}`
    );
    window.scrollTo({ top: 0, behavior: "smooth" });
  }

  return (
    <div className="flex gap-1 justify-center items-center mr-auto w-fit">
      {/* Go to previous page */}
      {currentPage > 1 && (
        <button
          onClick={() => navigateToPage(currentPage - 1)}
          className="w-10 h-10 flex justify-center items-center text-white bg-primary border-2 border-primary rounded-md hover:bg-white hover:text-primary transition-all duration-300 ease-in-out"
        >
          <GrPrevious className="text-lg font-bold" />
        </button>
      )}

      {/* Go to previous page by number */}
      {currentPage > 1 && (
        <button
          onClick={() => navigateToPage(currentPage - 1)}
          className="w-10 h-10 flex justify-center items-center text-white bg-primary border-2 border-primary rounded-md hover:bg-white hover:text-primary transition-all duration-300 ease-in-out"
        >
          <p className="text-lg font-bold">{currentPage - 1}</p>
        </button>
      )}

      {/* Current page */}
      <button className="w-10 h-10 flex justify-center items-center text-primary bg-white border-2 border-primary rounded-md">
        <p className="text-lg font-bold">{currentPage}</p>
      </button>

      {/* Go to next page by number */}
      {currentPage < totalPages && (
        <button
          onClick={() => navigateToPage(currentPage + 1)}
          className="w-10 h-10 flex justify-center items-center text-white bg-primary border-2 border-primary rounded-md hover:bg-white hover:text-primary transition-all duration-300 ease-in-out"
        >
          <p className="text-lg font-bold">{currentPage + 1}</p>
        </button>
      )}

      {/* Go to next page */}
      {currentPage < totalPages && (
        <button
          onClick={() => navigateToPage(currentPage + 1)}
          className="w-10 h-10 flex justify-center items-center text-white bg-primary border-2 border-primary rounded-md hover:bg-white hover:text-primary transition-all duration-300 ease-in-out"
        >
          <GrNext className="text-lg font-bold" />
        </button>
      )}
    </div>
  );
}
